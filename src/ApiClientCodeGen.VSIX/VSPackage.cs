using System;
using System.Runtime.InteropServices;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.CustomTool;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient
{
    [Guid("47AFE4E1-5A52-4FE1-8CA7-EDB8310BDA4A")]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("REST API Client Code Generator", "", "1.0")]
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
    public sealed class VsPackage : AsyncPackage
    {
        private readonly ICommandInitializer[] commands = {
            new AutoRestCodeGeneratorCustomToolSetter(),
            new NSwagCodeGeneratorCustomToolSetter(),
            new SwaggerCodeGeneratorCustomToolSetter(),
            new OpenApiCodeGeneratorCustomToolSetter(),
            new NSwagStudioCommand()
        };

        protected override async Task InitializeAsync(
            CancellationToken cancellationToken,
            IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);
            foreach (var command in commands)
                await command.InitializeAsync(this, cancellationToken);
        }
    }
}
