using System;
using System.ComponentModel.Design;
using System.Threading;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions
{
    public static class AsyncPackageExtensions
    {
        public static async Task SetupCommandAsync(
            this AsyncPackage package,
            Guid commandSet,
            int commandId,
            Action<DTE> action,
            CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? CancellationToken.None;
            await package.JoinableTaskFactory
                .SwitchToMainThreadAsync(token);

            var dteTask = package.GetServiceAsync(typeof(DTE));
            if (dteTask == null)
                return;
            var dte = await dteTask as DTE;
            
            var commandServiceTask = package.GetServiceAsync((typeof(IMenuCommandService)));
            if (commandServiceTask == null)
                return;
            
            var commandService = await commandServiceTask as IMenuCommandService;
            if (commandService == null)
                return;

            var menuCommand = new MenuCommand(
                (sender, e) =>
                {
                    ThreadHelper.ThrowIfNotOnUIThread();
                    action?.Invoke(dte);
                }, 
                new CommandID(commandSet, commandId));

            commandService.AddCommand(menuCommand);
        }
        
        public static async Task SetupCommandAsync(
            this AsyncPackage package,
            Guid commandSet,
            int commandId,
            Func<DTE, AsyncPackage, Task> func,
            CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? CancellationToken.None;
            await package.JoinableTaskFactory
                .SwitchToMainThreadAsync(token);

            var dteTask = package.GetServiceAsync(typeof(DTE));
            if (dteTask == null)
                return;
            var dte = await dteTask as DTE;
            
            var commandServiceTask = package.GetServiceAsync((typeof(IMenuCommandService)));
            if (commandServiceTask == null)
                return;
            
            var commandService = await commandServiceTask as IMenuCommandService;
            if (commandService == null)
                return;

            var menuCommand = new MenuCommand(
                async (sender, e) => await func.Invoke(dte, package), 
                new CommandID(commandSet, commandId));

            commandService.AddCommand(menuCommand);
        }
    }
}