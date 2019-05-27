using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Views;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using NuGet.VisualStudio;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew
{
    public class NewRestClientCommand : ICommandInitializer
    {
        public const string ContextGuid = "7CEC8679-C1B8-48BF-9FA4-5FAA38CBE0FA";
        public const string Name = "NSwag Studio Context";
        public const string Expression = "nswag";
        public const string TermValue = "HierSingleSelectionName:.nswag$";

        protected int CommandId { get; } = 0x100;
        protected Guid CommandSet { get; } = new Guid("E4B99F94-D11F-4CAA-ADCD-24302C232938");

        public async Task InitializeAsync(AsyncPackage package, CancellationToken token)
        {
            await package.SetupCommandAsync(CommandSet, CommandId, OnExecuteAsync, token);
        }

        private static async Task OnExecuteAsync(DTE dte, AsyncPackage package)
        {
            var result = EnterOpenApiSpecDialog.GetResult();
            if (result == null)
                return;

            var folder = FindFolder(ProjectExtensions.GetSelectedItem(), dte);
            if (string.IsNullOrWhiteSpace(folder))
            {
                Trace.WriteLine("Unable to get folder name");
                return;
            }

            var filePath = Path.Combine(folder, result.OutputFilename);
            File.WriteAllText(filePath, result.OpenApiSpecification);

            var fileInfo = new FileInfo(filePath);
            var project = ProjectExtensions.GetActiveProject(dte);
            var projectItem = project.AddFileToProject(dte, fileInfo);

            var customTool = result.SelectedCodeGenerator.GetCustomToolName();
            projectItem.Properties.Item("CustomTool").Value = customTool;

            await InstallDependenciesAsync(package, project, result.SelectedCodeGenerator);
        }

        private static async Task InstallDependenciesAsync(AsyncPackage package, Project project, SupportedCodeGenerator codeGenerator)
        {
            var componentModel = (IComponentModel) await package.GetServiceAsync(typeof(SComponentModel));
            var packageInstaller = componentModel.GetService<IVsPackageInstaller>();
            var installedServices = componentModel.GetService<IVsPackageInstallerServices>();
            var installedPackages = installedServices.GetInstalledPackages(project)?.ToList() ?? new List<IVsPackageMetadata>();
            
            var requiredPackages = codeGenerator.GetDependencies();
            foreach (var packageDependency in requiredPackages)
            {
                if (installedPackages.Any(
                    c => string.Equals(c.Id, packageDependency.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    if (installedPackages.Any(c => c.VersionString == packageDependency.Version.ToString(3)))
                        continue;
                }

                packageInstaller.InstallPackage(
                    null,
                    project,
                    packageDependency.Name,
                    packageDependency.Version,
                    false);
            }
        }

        private static string FindFolder(object item, DTE dte)
        {
            switch (item)
            {
                case ProjectItem projectItem:
                    return File.Exists(projectItem.FileNames[1])
                        ? Path.GetDirectoryName(projectItem.FileNames[1])
                        : projectItem.FileNames[1];

                case Project project:
                    return project.GetRootFolder(dte);

                default:
                    return null;
            }
        }
    }
}
