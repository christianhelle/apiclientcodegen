using System;
using System.ComponentModel.Design;
using EnvDTE;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands
{
    public abstract class CustomToolSetter<T>
        : ICommandInitializer
        where T : IVsSingleFileGenerator
    {
        private DTE dte;

        protected abstract int CommandId { get; }
        protected Guid CommandSet { get; } = new Guid("C292653B-5876-4B8C-B672-3375D8561881");

        public async Task InitializeAsync(
            AsyncPackage package,
            CancellationToken token)
        {
            await package.JoinableTaskFactory.SwitchToMainThreadAsync(token);
            
            var dteTask = package.GetServiceAsync(typeof(DTE));
            if (dteTask == null)
                return;
            
            dte = await dteTask as DTE;
            if (dte == null)
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
            ThreadHelper.ThrowIfNotOnUIThread();
            var item = dte.SelectedItems.Item(1).ProjectItem;
            item.Properties.Item("CustomTool").Value = typeof(T).Name;
        }
    }
}