using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.NSwagStudio
{
    [ExcludeFromCodeCoverage]
    public class NSwagStudioCommand : ICommandInitializer
    {
        public const string ContextGuid = "65B3A74F-CD47-476A-A992-0C3DE31455FD";
        public const string Name = "NSwag Studio Context";
        public const string Expression = "nswag";
        public const string TermValue = "HierSingleSelectionName:.nswag$";

        protected int CommandId { get; } = 0x100;
        protected Guid CommandSet { get; } = new Guid("F76783DA-7AE3-4EDB-BDF4-B580CAB1BA90");

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
                throw new RunNSwagStudioCommandException(GetType().Name, e);
            }
        }

        private static async Task OnExecuteAsync(DTE dte, AsyncPackage package)
        {
            Logger.Instance.TrackFeatureUsage("Generate NSwag Studio output");

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var item = dte.SelectedItems.Item(1).ProjectItem;
            var nswagStudioFile = item.FileNames[0];

            var codeGenerator = new NSwagStudioCodeGenerator(
                nswagStudioFile,
                new CustomPathOptions(),
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader())));
            
            codeGenerator.GenerateCode(null);

            var project = dte.GetActiveProject()!;
            await project.InstallMissingPackagesAsync(package, SupportedCodeGenerator.NSwag);
        }
    }
}