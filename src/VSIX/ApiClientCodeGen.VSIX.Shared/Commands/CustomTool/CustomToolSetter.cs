using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.CustomTool
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

        private async Task ExecuteAsync(AsyncPackage package)
        {
            try
            {
                await OnExecuteAsync(package);
            }
            catch (Exception e)
            {
                throw new CustomToolCommandException(GetType().Name, e);
            }
        }

        protected virtual async Task OnExecuteAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var item = await VS.Solutions.GetActiveItemAsync();
            var file = await PhysicalFile.FromFileAsync(item.FullPath);
            await file.TrySetAttributeAsync("CustomTool", typeof(T).Name);

            var name = typeof(T).Name.Replace("CodeGenerator", string.Empty);
            Trace.WriteLine($"Generating code using {name}");

            var project = await VS.Solutions.GetActiveProjectAsync();
            await project.InstallMissingPackagesAsync(
                typeof(T).GetSupportedCodeGenerator());
        }
    }
}