using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
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

        private async Task ExecuteAsync(AsyncPackage package)
        {
            try
            {
                await OnExecuteAsync();
            }
            catch (Exception e)
            {
                throw new RunNSwagStudioCommandException(GetType().Name, e);
            }
        }

        private static async Task OnExecuteAsync()
        {
            Logger.Instance.TrackFeatureUsage("Generate NSwag Studio output");

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var project = await VS.Solutions.GetActiveProjectAsync();
            await project.InstallMissingPackagesAsync(SupportedCodeGenerator.NSwag);

            var item = await VS.Solutions.GetActiveItemAsync();
            var nswagStudioFile = item.FullPath;

            var codeGenerator = new NSwagStudioCodeGenerator(
                nswagStudioFile,
                new CustomPathOptions(),
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader())));

            codeGenerator.GenerateCode(null);
        }
    }
}