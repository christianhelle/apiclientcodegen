using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using Microsoft.VisualStudio.Shell;
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
            Func<AsyncPackage, Task> func,
            CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? CancellationToken.None;
            await package.JoinableTaskFactory
                .SwitchToMainThreadAsync(token);

            if (!(await package.GetServiceAsync(typeof(IMenuCommandService)) is IMenuCommandService commandService))
                return;

            var menuCommand = new MenuCommand(
                async (sender, e) => await InvokeAsync(package, func), 
                new CommandID(commandSet, commandId));

            commandService.AddCommand(menuCommand);
        }

        private static async Task InvokeAsync(AsyncPackage package, Func<AsyncPackage, Task> func)
        {
            try
            {
                await func.Invoke(package);
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Trace.TraceError(e.ToString());
            }
        }
    }
}