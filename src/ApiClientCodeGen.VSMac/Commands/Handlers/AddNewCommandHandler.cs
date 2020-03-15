using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet;
using Microsoft.VisualStudio.Threading;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using MonoDevelop.Projects;
using PackageDependency = ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet.PackageDependency;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public abstract class AddNewCommandHandler : BaseCommandHandler
    {
        private readonly IProcessLauncher process;
        private readonly PackageDependencyListProvider dependencyProvider;

        protected AddNewCommandHandler()
            : this(
                Container.Instance.Resolve<IProcessLauncher>(),
                Container.Instance.Resolve<PackageDependencyListProvider>())
        {
        }

        protected AddNewCommandHandler(
            IProcessLauncher processLauncher,
            PackageDependencyListProvider dependencyListProvider)
        {
            process = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            dependencyProvider = dependencyListProvider ?? throw new ArgumentNullException(nameof(dependencyListProvider));
        }

        protected abstract string GeneratorName { get; }

        protected override void Run()
            => AddNewSwaggerFile().Forget();

        protected override void Run(object dataItem)
            => AddNewSwaggerFile().Forget();

        protected override void Update(CommandInfo info)
            => info.Visible =
                IdeApp.ProjectOperations.CurrentSelectedItem is Project ||
                IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFolder;

        private async Task AddNewSwaggerFile()
        {
            var url = MessageService.GetTextResponse(
                "Enter the URL to the Swagger / Open API spec file",
                "Add New REST API Client",
                "https://petstore.swagger.io/v2/swagger.json");

            if (string.IsNullOrWhiteSpace(url))
                return;

            var path = string.Empty;
            var project = IdeApp.ProjectOperations.CurrentSelectedItem as Project;
            if (project == null)
            {
                if (IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFolder folder)
                {
                    project = folder.Project;
                    path = folder.Path;
                }

                if (project == null)
                    return;

                if (string.IsNullOrWhiteSpace(path))
                    path = project.ItemDirectory;
            }
            else
            {
                path = project.ItemDirectory;
            }

            await AddRequiredPackages(project);
            await AddFile(project, path, url);

            project.NotifyModified(string.Empty);
            project.ReloadProjectBuilder();
        }

        protected abstract SupportedCodeGenerator CodeGeneratorType { get; }

        private async Task AddRequiredPackages(Project project)
        {
            foreach (var package in dependencyProvider.GetDependencies(CodeGeneratorType))
            {
                var arguments = $"add package {package.Name} --version {package.Version}";
                await Task.Run(() => process.Start("dotnet", arguments, project.ItemDirectory));
            }
        }

        protected virtual async Task AddFile(
            Project project,
            string itemPath,
            string url)
        {
            var filename = Path.Combine(itemPath, "Swagger.json");
            var contents = await DownloadTextAsync(url);
            File.WriteAllText(filename, contents);

            var item = project.AddFile(filename, "None");
            item.Generator = GeneratorName;

            IdeApp.ProjectOperations.MarkFileDirty(item.FilePath);
        }

        protected static async Task<string> DownloadTextAsync(string url)
        {
            using (var client = new WebClient())
                return await client.DownloadStringTaskAsync(
                    new Uri(url));
        }
    }
}