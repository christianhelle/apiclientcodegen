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
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.Kiota;
using Rapicgen.Core.Generators;
using Rapicgen.Options;
using Rapicgen.Options.Kiota;

namespace Rapicgen.Commands.Kiota
{
    [ExcludeFromCodeCoverage]
    public class KiotaCommand : ICommandInitializer
    {
        public const string ContextGuid = "9FA75FC4-9B4D-4A81-855E-DF85C929A453";
        public const string Name = "Kiota Context";
        public const string Expression = "json";
        public const string TermValue = "HierSingleSelectionName:kiota-lock.json$";

        protected int CommandId { get; } = 0x300;
        protected Guid CommandSet { get; } = new Guid("80A6CDF2-29A2-4941-AD88-8CF36223F9A5");

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
                throw new RunKiotaCommandException(GetType().Name, e);
            }
        }

        private static async Task OnExecuteAsync(DTE dte, AsyncPackage package)
        {
            Logger.Instance.TrackFeatureUsage("Generate Kiota output");

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var item = dte.SelectedItems.Item(1).ProjectItem;
            var KiotaSettingsFile = item.FileNames[0];

            var codeGenerator = new KiotaCodeGenerator(
                KiotaSettingsFile,
                default,
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()),
                new OptionsFactory().Create<IKiotaOptions, KiotaOptionsPage, DefaultKiotaOptions>());
            codeGenerator.GenerateCode(null);

            var project = dte.GetActiveProject()!;
            await project.InstallMissingPackagesAsync(package, SupportedCodeGenerator.Kiota);
        }
    }
}