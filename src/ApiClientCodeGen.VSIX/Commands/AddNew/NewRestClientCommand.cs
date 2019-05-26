using System;
using System.ComponentModel.Design;
using System.IO;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Utility;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
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
        }
    }
}
