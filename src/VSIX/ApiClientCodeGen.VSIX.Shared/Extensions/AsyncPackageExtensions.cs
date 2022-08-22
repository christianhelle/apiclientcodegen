using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AsyncPackageExtensions
    {
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
            var dte = (DTE)await dteTask;
            
            var commandServiceTask = package.GetServiceAsync((typeof(IMenuCommandService)));
            if (commandServiceTask == null)
                return;
            
            var commandService = await commandServiceTask as IMenuCommandService;
            if (commandService == null)
                return;

            var menuCommand = new MenuCommand(
                (_, _) => InvokeAsync(package, func, dte).Forget(), 
                new CommandID(commandSet, commandId));

            commandService.AddCommand(menuCommand);
        }

        private static async Task InvokeAsync(AsyncPackage package, Func<DTE, AsyncPackage, Task> func, DTE dte)
        {
            try
            {
                await func.Invoke(dte, package);
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Trace.TraceError(e.ToString());
            }
        }
    }
}