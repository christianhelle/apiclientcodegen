using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Rapicgen.Core;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.NuGet;
using Microsoft.VisualStudio.Threading;
using Mono.Addins;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using MonoDevelop.Projects;
using PackageDependency = Rapicgen.Core.NuGet.PackageDependency;
using System.Diagnostics.CodeAnalysis;

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
            dependencyProvider =
                dependencyListProvider ?? throw new ArgumentNullException(nameof(dependencyListProvider));
        }

        protected abstract string GeneratorName { get; }

        [SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = "<Pending>")]
        protected override async void Run()
        {
            try
            {
                await AddNewSwaggerFileAsync();
            }
            catch
            {
                // Ignore
            }
        }

        [SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = "<Pending>")]
        protected override async void Run(object dataItem)
        {
            try
            {
                await AddNewSwaggerFileAsync();
            }
            catch
            {
                // Ignore
            }
        }

        protected override void Update(CommandInfo info)
            => info.Visible =
                IdeApp.ProjectOperations.CurrentSelectedItem is Project ||
                IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFolder;

        private async Task AddNewSwaggerFileAsync()
        {
            Logger.Instance.TrackFeatureUsage("New REST API Client", "VSMac");

            var url = MessageService.GetTextResponse(
                "Enter the URL to the Swagger / Open API spec file",
                "Add New REST API Client",
                "https://petstore.swagger.io/v2/swagger.json");

            if (string.IsNullOrWhiteSpace(url))
                return;

            var project = GetCurrentProject();
            string path = IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFolder folder
                ? folder.Path
                : project.ItemDirectory;

            await AddRequiredPackages(project);
            await AddFile(path, url);
        }

        private static Project GetCurrentProject()
        {
            var project = IdeApp.ProjectOperations.CurrentSelectedItem as Project;
            if (project != null)
                return project;

            if (IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFolder folder)
                project = folder.Project;

            return project;
        }

        protected abstract SupportedCodeGenerator CodeGeneratorType { get; }

        private async Task AddRequiredPackages(Project project)
        {
            foreach (var package in dependencyProvider.GetDependencies(CodeGeneratorType))
            {
                var arguments = $"add package {package.Name} --version {package.Version}";
                await Task.Run(() => process.Start("dotnet", arguments, project.ItemDirectory));
            }

            project.NotifyModified(string.Empty);
            await project.RefreshProjectBuilder();
        }

        protected virtual async Task AddFile(string itemPath, string url)
        {
            var filename = Path.Combine(itemPath, $"Swagger{Path.GetExtension(url)}");
            var contents = await DownloadTextAsync(url);
            File.WriteAllText(filename, contents);

            var item = IdeApp.ProjectOperations.CurrentSelectedProject.AddFile(filename);
            item.BuildAction = BuildAction.None;
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