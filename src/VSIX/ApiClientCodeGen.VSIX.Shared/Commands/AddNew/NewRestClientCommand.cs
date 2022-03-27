using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew
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

        private async Task ExecuteAsync(AsyncPackage package)
        {
            try
            {
                await OnExecuteAsync(package);
            }
            catch (Exception e)
            {
                throw new AddNewCommandException(GetType().Name, e);
            }
        }

        private async Task OnExecuteAsync(AsyncPackage package)
        {
            Logger.Instance.TrackFeatureUsage($"New REST API Client ({CodeGenerator.GetName()})");

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var result = EnterOpenApiSpecDialog.GetResult();
            if (result == null)
                return;

            var selectedItem = ProjectExtensions.GetSelectedItem();
            var folder = FindFolder(selectedItem);
            if (string.IsNullOrWhiteSpace(folder))
            {
                Trace.WriteLine("Unable to get folder name");
                return;
            }

            var contents = result.OpenApiSpecification;
            var filename = $"{result.OutputFilename}{Path.GetExtension(result.Url)}";

            var project = await VS.Solutions.GetActiveProjectAsync();
            await OnInstallPackagesAsync(package, project, result);

            if (CodeGenerator == SupportedCodeGenerator.NSwagStudio)
            {
                var outputNamespace = project.GetTopLevelNamespace();
                contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                    result,
                    new NSwagStudioOptions(),
                    outputNamespace);
                filename = filename.Replace(".json", ".nswag");
            }

            var filePath = Path.Combine(folder, filename);
            File.WriteAllText(filePath, contents);

            var fileInfo = new FileInfo(filePath);
            await project.AddExistingFilesAsync(fileInfo.FullName);

            var file = await PhysicalFile.FromFileAsync(fileInfo.FullName);
            await file.TrySetAttributeAsync("BuildAction", "None");

            if (CodeGenerator != SupportedCodeGenerator.NSwagStudio)
            {
                var customTool = CodeGenerator.GetCustomToolName();
                await file.TrySetAttributeAsync("CustomTool", customTool);
            }
            else
            {
                var generator = new NSwagStudioCodeGenerator(
                    filePath,
                    new CustomPathOptions(),
                    new ProcessLauncher(),
                    new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader())));

                generator.GenerateCode(null);

                dynamic nswag = JsonConvert.DeserializeObject(contents);
                var nswagOutput = nswag.codeGenerators.swaggerToCSharpClient.output.ToString();
                project.AddExistingFilesAsync(Path.Combine(folder, nswagOutput));
            }
        }

        protected virtual Task OnInstallPackagesAsync(
            AsyncPackage package,
            Community.VisualStudio.Toolkit.Project project,
            EnterOpenApiSpecDialogResult dialogResult)
            => project.InstallMissingPackagesAsync(CodeGenerator);

        private static string FindFolder(object item)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            switch (item)
            {
                case ProjectItem projectItem:
                    return File.Exists(projectItem.FileNames[1])
                        ? Path.GetDirectoryName(projectItem.FileNames[1])
                        : projectItem.FileNames[1];

                case Community.VisualStudio.Toolkit.Project project:
                    return Path.GetDirectoryName(project.FullPath);

                default:
                    return null;
            }
        }
    }
}