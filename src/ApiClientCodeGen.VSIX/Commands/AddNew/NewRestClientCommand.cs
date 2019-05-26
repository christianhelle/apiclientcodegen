using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Utility;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Views;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
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

        private DTE _dte;

        public async Task InitializeAsync(AsyncPackage package, CancellationToken token)
        {
            await package.JoinableTaskFactory.SwitchToMainThreadAsync(token);

            var dteTask = package.GetServiceAsync(typeof(DTE));
            if (dteTask == null)
                return;

            _dte = await dteTask as DTE;
            if (_dte == null)
                return;

            var commandServiceTask = package.GetServiceAsync((typeof(IMenuCommandService)));
            if (commandServiceTask == null)
                return;

            var commandService = await commandServiceTask as IMenuCommandService;
            if (commandService == null)
                return;

            var cmdId = new CommandID(CommandSet, CommandId);
            var cmd = new MenuCommand(OnExecute, cmdId);
            commandService.AddCommand(cmd);
        }

        private void OnExecute(object sender, EventArgs e)
        {
            var result = EnterOpenApiSpecDialog.GetResult();
            if (result == null)
                return;

            var folder = FindFolder(ProjectHelpers.GetSelectedItem());
            if (string.IsNullOrWhiteSpace(folder))
            {
                Trace.WriteLine("Unable to get folder name");
                return;
            }

            var filePath = Path.Combine(folder, result.OutputFilename);
            File.WriteAllText(filePath, result.OpenApiSpecification);

            var fileInfo = new FileInfo(filePath);
            var project = ProjectHelpers.GetActiveProject();
            var projectItem = project.AddFileToProject(fileInfo);

            var customTool = result.SelectedCodeGenerator.GetCustomToolName();
            projectItem.Properties.Item("CustomTool").Value = customTool;
        }

        private static string FindFolder(object item)
        {
            switch (item)
            {
                case ProjectItem projectItem:
                {
                    var fileName = projectItem.FileNames[1];
                    return File.Exists(fileName) ? Path.GetDirectoryName(fileName) : fileName;
                }

                case Project project:
                    return project.GetRootFolder();

                default:
                    return null;
            }
        }
    }
}
