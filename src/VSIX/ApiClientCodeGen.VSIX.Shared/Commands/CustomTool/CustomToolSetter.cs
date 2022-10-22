using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Rapicgen.Core.Exceptions;
using Rapicgen.CustomTool;
using Rapicgen.Extensions;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace Rapicgen.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public abstract class CustomToolSetter<T>
        : ICommandInitializer
        where T : SingleFileCodeGenerator
    {
        protected abstract int CommandId { get; }
        protected Guid CommandSet { get; } = new Guid("C292653B-5876-4B8C-B672-3375D8561881");

        public Task InitializeAsync(
            AsyncPackage package,
            CancellationToken token)
            => package.SetupCommandAsync(
                CommandSet,
                CommandId,
                ExecuteAsync,
                token);

        private async Task ExecuteAsync(DTE dte, AsyncPackage package)
        {
            try
            {
                await OnExecuteAsync(dte, package);
            }
            catch (Exception e)
            {
                throw new CustomToolCommandException(GetType().Name, e);
            }
        }

        protected virtual async Task OnExecuteAsync(DTE dte, AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var item = dte.SelectedItems.Item(1).ProjectItem;
            item.Properties.Item("CustomTool").Value = typeof(T).Name;

            var name = typeof(T).Name.Replace("CodeGenerator", string.Empty);
            Trace.WriteLine($"Generating code using {name}");

            var project = dte.GetActiveProject();
            if (project != null)
                await project.InstallMissingPackagesAsync(
                    package,
                    typeof(T).GetSupportedCodeGenerator());
        }
    }
}