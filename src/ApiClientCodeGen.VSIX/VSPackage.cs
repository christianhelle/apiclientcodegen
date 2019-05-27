using System;
using System.Runtime.InteropServices;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.CustomTool;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.NSwagStudio;
using Microsoft.VisualStudio.Shell;
using OutputWindow = ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Utility.OutputWindow;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient
{
    [Guid("47AFE4E1-5A52-4FE1-8CA7-EDB8310BDA4A")]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(VsixName, "", "1.0")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideUIContextRule(
        CustomToolSetterCommand.ContextGuid,
        CustomToolSetterCommand.Name,
        CustomToolSetterCommand.Expression,
        new[] { CustomToolSetterCommand.Expression },
        new[] { CustomToolSetterCommand.TermValue })]
    [ProvideUIContextRule(
        NSwagStudioCommand.ContextGuid,
        NSwagStudioCommand.Name,
        NSwagStudioCommand.Expression,
        new[] { NSwagStudioCommand.Expression },
        new[] { NSwagStudioCommand.TermValue })]
    [ProvideUIContextRule(
        NewRestClientCommand.ContextGuid,
        NewRestClientCommand.Name,
        NewRestClientCommand.Expression,
        new[] { NewRestClientCommand.Expression },
        new[] { NewRestClientCommand.TermValue })]
    public sealed class VsPackage : AsyncPackage
    {
        public const string VsixName = "REST API Client Code Generator";

        private readonly ICommandInitializer[] commands = {
            new AutoRestCodeGeneratorCustomToolSetter(),
            new NSwagCodeGeneratorCustomToolSetter(),
            new SwaggerCodeGeneratorCustomToolSetter(),
            new OpenApiCodeGeneratorCustomToolSetter(),
            new NSwagStudioCommand(),
            new NewRestClientCommand()
        };

        protected override async Task InitializeAsync(
            CancellationToken cancellationToken,
            IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await base.InitializeAsync(cancellationToken, progress);
            OutputWindow.Initialize(this, VsixName);

            foreach (var command in commands)
                await command.InitializeAsync(this, cancellationToken);
        }
    }
}
