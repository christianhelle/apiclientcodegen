using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Rapicgen.Core;
using Rapicgen.Core.Exceptions;
using Rapicgen.Core.Logging;
using Rapicgen.Extensions;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;
using Rapicgen.Core.Generators.Refitter;

namespace Rapicgen.Commands.Refitter
{
    [ExcludeFromCodeCoverage]
    public class RefitterCommand : ICommandInitializer
    {
        public const string ContextGuid = "2A5A0DB6-FC9C-48AB-98E5-C69D7157CEF5";
        public const string Name = "Refitter Context";
        public const string Expression = "refitter";
        public const string TermValue = "HierSingleSelectionName:.refitter";

        protected int CommandId { get; } = 0x200;
        protected Guid CommandSet { get; } = new Guid("E00623E7-31F9-40E9-BDEE-9D7829D09184");

        public Task InitializeAsync(AsyncPackage package, CancellationToken token)
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
                throw new RunRefitterCommandException(GetType().Name, e);
            }
        }

        private static async Task OnExecuteAsync(DTE dte, AsyncPackage package)
        {
            Logger.Instance.TrackFeatureUsage("Generate Refitter output");

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var item = dte.SelectedItems.Item(1).ProjectItem;
            var refitterSettingsFile = item.FileNames[0];

            var codeGenerator = new RefitterCodeGenerator(refitterSettingsFile, default, default);
            codeGenerator.GenerateCode(null);

            var project = dte.GetActiveProject()!;
            await project.InstallMissingPackagesAsync(package, SupportedCodeGenerator.Refitter);
        }
    }
}