using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using Rapicgen.Commands;
using Rapicgen.Commands.AddNew;
using Rapicgen.Commands.CustomTool;
using Rapicgen.Commands.NSwagStudio;
using Rapicgen.Core.Logging;
using Rapicgen.Options.Analytics;
using Rapicgen.Options.AutoRest;
using Rapicgen.Options.General;
using Rapicgen.Options.NSwag;
using Rapicgen.Options.NSwagStudio;
using Rapicgen.Options.OpenApiGenerator;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;
using Rapicgen.Options.Refitter;
using Rapicgen.Commands.Refitter;
using Rapicgen.Options.Kiota;
using Rapicgen.Commands.Kiota;

namespace Rapicgen
{
    [ExcludeFromCodeCoverage]
    [Guid("47AFE4E1-5A52-4FE1-8CA7-EDB8310BDA4A")]
    [InstalledProductRegistration(VsixName, "", "1.0")]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(UIContextGuids.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideUIContextRule(
        CustomToolSetterCommand.ContextGuid,
        CustomToolSetterCommand.Name,
        CustomToolSetterCommand.Expression,
        new[] { CustomToolSetterCommand.TermNameJson, CustomToolSetterCommand.TermNameYaml },
        new[] { CustomToolSetterCommand.TermValueJson, CustomToolSetterCommand.TermValueYaml })]
    [ProvideUIContextRule(
        NSwagStudioCommand.ContextGuid,
        NSwagStudioCommand.Name,
        NSwagStudioCommand.Expression,
        new[] { NSwagStudioCommand.Expression },
        new[] { NSwagStudioCommand.TermValue })]
    [ProvideUIContextRule(
        RefitterCommand.ContextGuid,
        RefitterCommand.Name,
        RefitterCommand.Expression,
        new[] { RefitterCommand.Expression },
        new[] { RefitterCommand.TermValue })]
    [ProvideUIContextRule(
        KiotaCommand.ContextGuid,
        KiotaCommand.Name,
        KiotaCommand.Expression,
        new[] { KiotaCommand.Expression },
        new[] { KiotaCommand.TermValue })]
    [ProvideOptionPage(
        typeof(GeneralOptionPage),
        VsixName,
        GeneralOptionPage.Name,
        0,
        0,
        true)]
    [ProvideOptionPage(
        typeof(AutoRestOptionsPage),
        VsixName,
        AutoRestOptionsPage.Name,
        0,
        0,
        true)]
    [ProvideOptionPage(
        typeof(NSwagOptionsPage),
        VsixName,
        NSwagOptionsPage.Name,
        0,
        0,
        true)]
    [ProvideOptionPage(
        typeof(NSwagStudioOptionsPage),
        VsixName,
        NSwagStudioOptionsPage.Name,
        0,
        0,
        true)]
    [ProvideOptionPage(
        typeof(AnalyticsOptionPage),
        VsixName,
        AnalyticsOptionPage.Name,
        0,
        0,
        true)]
    [ProvideOptionPage(
        typeof(OpenApiGeneratorOptionsPage),
        VsixName,
        OpenApiGeneratorOptionsPage.Name,
        0,
        0,
        true)]
    [ProvideOptionPage(
        typeof(RefitterOptionsPage),
        VsixName,
        RefitterOptionsPage.Name,
        0,
        0,
        true)]
    [ProvideOptionPage(
        typeof(KiotaOptionsPage),
        VsixName,
        KiotaOptionsPage.Name,
        0,
        0,
        true)]
    public sealed class VsPackage : AsyncPackage
    {
        public const string VsixName = "REST API Client Code Generator";

        private readonly ICommandInitializer[] commands = {
            new AutoRestCodeGeneratorCustomToolSetter(),
            new NSwagCodeGeneratorCustomToolSetter(),
            new SwaggerCodeGeneratorCustomToolSetter(),
            new OpenApiCodeGeneratorCustomToolSetter(),
            new KiotaCodeGeneratorCustomToolSetter(),
            new RefitterCodeGeneratorCustomToolSetter(),
            new NSwagStudioCommand(),
            new RefitterCommand(),
            new KiotaCommand(),
            new NewAutoRestClientCommand(),
            new NewNSwagClientCommand(),
            new NewSwaggerClientCommand(),
            new NewOpenApiClientCommand(),
            new NewNSwagStudioClientCommand(),
            new NewKiotaClientCommand(),
            new NewRefitterClientCommand()
        };

        public static AsyncPackage Instance { get; private set; } = null!;

        public static Version VisualStudioVersion { get; private set; } = null!;

        protected override async Task InitializeAsync(
            CancellationToken cancellationToken,
            IProgress<ServiceProgressData> progress)
        {
            Logger
                .Setup(new OutputWindowRemoteLogger())
                .WithDefaultTags("VSIX");

            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await base.InitializeAsync(cancellationToken, progress);
            Instance = this;

            foreach (var command in commands)
                await command.InitializeAsync(this, cancellationToken);

            var telemetryOptions = GetDialogPage(typeof(AnalyticsOptionPage)) as ITelemetryOptions;
            if (telemetryOptions?.TelemetryOptOut == true)
                Logger.Instance.Disable();
            else
                await TrySetupVersionTrackingAsync(cancellationToken);
        }

        private async Task TrySetupVersionTrackingAsync(CancellationToken cancellationToken)
        {
            try
            {
                await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
                var shell = (IVsShell)(await GetServiceAsync(typeof(SVsShell)))!;
                shell.GetProperty((int)__VSSPROPID5.VSSPROPID_ReleaseVersion, out object value);
                if (value is string raw)
                {
                    VisualStudioVersion = Version.Parse(raw.Split(' ')[0]);
                    Logger.GetLogger<AppInsightsRemoteLogger>()
                        .AddTelemetryInitializer(
                            new VisualStudioVersionInitializer(VisualStudioVersion));
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteLine("Failed to setup version tracking");
                Logger.Instance.TrackError(e);
            }
        }
    }
}
