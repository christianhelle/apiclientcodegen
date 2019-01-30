using System;
using System.Runtime.InteropServices;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient
{
    [Guid("47AFE4E1-5A52-4FE1-8CA7-EDB8310BDA4A")]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("REST API Client Code Generator", "", "1.0")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideUIContextRule(
        CustomToolSetterBase.ContextGuid,
        CustomToolSetterBase.Name,
        CustomToolSetterBase.Expression,
        new[] { CustomToolSetterBase.Expression },
        new[] { CustomToolSetterBase.TermValue })]
    public sealed class VsPackage : AsyncPackage
    {
        private readonly ICustomToolSetter[] commands = {
            new AutoRestCodeGeneratorCustomToolSetter(),
            new NSwagCodeGeneratorCustomToolSetter(),
            new SwaggerCodeGeneratorCustomToolSetter()
        };

        protected override async Task InitializeAsync(
            CancellationToken cancellationToken,
            IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await base.InitializeAsync(cancellationToken, progress);
            
            foreach (var command in commands)
                await command.InitializeAsync(this);
        }
    }
}
