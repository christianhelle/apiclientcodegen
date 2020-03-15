using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public abstract class AddNewCommandHandler : BaseCommandHandler
    {
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

            await AddFile(project, path, url);
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