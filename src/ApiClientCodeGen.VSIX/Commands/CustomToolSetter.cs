using System;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands
{
    public abstract class CustomToolSetter<T>
        : ICustomToolSetter
        where T : IVsSingleFileGenerator
    {
        private DTE dte;

        protected abstract int CommandId { get; }
        protected abstract Guid CommandSet { get; }

        public async Task InitializeAsync(AsyncPackage package)
        {
            dte = (DTE)await package.GetServiceAsync(typeof(DTE));
            var commandService = (IMenuCommandService)await package.GetServiceAsync((typeof(IMenuCommandService)));
            var cmdId = new CommandID(CommandSet, CommandId);
            var cmd = new MenuCommand(OnExecute, cmdId);
            commandService.AddCommand(cmd);
        }

        private void OnExecute(object sender, EventArgs e)
        {
            var item = dte.SelectedItems.Item(1).ProjectItem;
            item.Properties.Item("CustomTool").Value = typeof(T).Name;
        }
    }
}