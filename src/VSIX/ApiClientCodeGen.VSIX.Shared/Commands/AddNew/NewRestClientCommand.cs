using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Rapicgen.Core;
using Rapicgen.Core.Exceptions;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwagStudio;
using Rapicgen.Core.Logging;
using Rapicgen.Extensions;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Installer;
using Rapicgen.Options.General;
using Rapicgen.Options.NSwagStudio;
using Rapicgen.Windows;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using VSLangProj;
using Task = System.Threading.Tasks.Task;

namespace Rapicgen.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public abstract class NewRestClientCommand : ICommandInitializer
    {
        protected Guid CommandSet { get; } = new Guid("E4B99F94-D11F-4CAA-ADCD-24302C232938");

        protected virtual int CommandId { get; } = 0x100;
        protected abstract SupportedCodeGenerator CodeGenerator { get; }

        public Task InitializeAsync(AsyncPackage package, CancellationToken token)
            => package.SetupCommandAsync(
                CommandSet,
                CommandId,
                ExecuteAsync,
                token);

        private async Task ExecuteAsync(DTE dte, AsyncPackage package)
        {
            try
            {
                await OnExecuteAsync(dte, package);
            }
            catch (Exception e)
            {
                throw new AddNewCommandException(GetType().Name, e);
            }
        }

        private async Task OnExecuteAsync(DTE dte, AsyncPackage package)
        {
            Logger.Instance.TrackFeatureUsage($"New REST API Client ({CodeGenerator.GetName()})");

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var result = EnterOpenApiSpecDialog.GetResult();
            if (result == null)
                return;

            var selectedItem = ProjectExtensions.GetSelectedItem();
            if (selectedItem == null)
            {
                Logger.Instance.WriteLine("Nothing is selected");
                return;
            }

            var folder = FindFolder(selectedItem, dte);
            if (string.IsNullOrWhiteSpace(folder))
            {
                Logger.Instance.WriteLine("Unable to get folder name");
                return;
            }

            var contents = result.OpenApiSpecification;
            var filename = $"{result.OutputFilename}{Path.GetExtension(result.Url)}";

            if (CodeGenerator == SupportedCodeGenerator.NSwagStudio)
            {
                var outputNamespace = dte.GetActiveProject()?.GetTopLevelNamespace();
                contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                    result,
                    new NSwagStudioOptions(),
                    outputNamespace);
                filename = filename.Replace(".json", ".nswag");
            }

            var filePath = Path.Combine(folder, filename);
            File.WriteAllText(filePath, contents);

            var fileInfo = new FileInfo(filePath);
            var project = dte.GetActiveProject();
            var projectItem = project?.AddFileToProject(dte, fileInfo, "None");
            if (projectItem != null)
            {
                projectItem.Properties.Item("BuildAction").Value = prjBuildAction.prjBuildActionNone;

                if (CodeGenerator != SupportedCodeGenerator.NSwagStudio)
                {
                    var customTool = CodeGenerator.GetCustomToolName();
                    projectItem.Properties.Item("CustomTool").Value = customTool;
                }
                else
                {
                    var generator = new NSwagStudioCodeGenerator(
                        filePath,
                        new CustomPathOptions(),
                        new ProcessLauncher(),
                        new DependencyInstaller(
                            new NpmInstaller(new ProcessLauncher()),
                            new FileDownloader(new WebDownloader()),
                            new ProcessLauncher()));

                    generator.GenerateCode(null!);

                    dynamic nswag = JsonConvert.DeserializeObject(contents)!;
                    var nswagOutput = nswag.codeGenerators.swaggerToCSharpClient.output.ToString();
                    project!.AddFileToProject(dte, new FileInfo(Path.Combine(folder, nswagOutput)));
                }
            }

            if (project != null)
                await OnInstallPackagesAsync(package, project, result);
        }

        protected virtual async Task OnInstallPackagesAsync(
            AsyncPackage package,
            Project project,
            EnterOpenApiSpecDialogResult dialogResult)
        {
            await project.InstallMissingPackagesAsync(package, CodeGenerator);
        }

        private static string? FindFolder(object item, DTE dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            switch (item)
            {
                case ProjectItem projectItem:
                    return (File.Exists(projectItem.FileNames[1])
                        ? Path.GetDirectoryName(projectItem.FileNames[1])
                        : projectItem.FileNames[1])!;

                case Project project:
                    return project.GetRootFolder(dte);

                default:
                    return null;
            }
        }
    }
}