using ApiClientCodeGen.VSIX.Extensibility.Commands.Placements;
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Logging;

namespace ApiClientCodeGen.VSIX.Extensibility;

[VisualStudioContribution]
internal class ExtensionEntrypoint : Extension
{
    const string ExtensionName = "REST API Client Code Generator (PREVIEW)";

    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new(
            id: "f7530eb1-1ce9-46ac-8fab-165b68cf3d61",
            version: ExtensionAssemblyVersion,
            publisherName: "Christian Resma Helle",
            displayName: ExtensionName,
            description: "Generate REST API client code from OpenAPI/Swagger specifications")
        {
            Icon = "icon.png",
            License = "LICENSE.txt",
        },
    };

    [VisualStudioContribution]
    public static MenuConfiguration GenerateMenu
        => new("%ApiClientCodeGenerator.GroupDisplayName%")
        {
            Placements =
            [
                KnownPlacements.ItemNode,
                KnownPlacements.Node_IncludeExcludeGroup
            ],
            Children =
            [
                MenuChild.Command<Commands.GenerateRefitterCommand>(),
                MenuChild.Command<Commands.GenerateNSwagCommand>(),
                MenuChild.Command<Commands.GenerateOpenApiCommand>(),
                MenuChild.Command<Commands.GenerateKiotaCommand>(),
                MenuChild.Command<Commands.GenerateSwaggerCommand>(),
                MenuChild.Command<Commands.GenerateAutoRestCommand>(),
                MenuChild.Separator,
                MenuChild.Command<Commands.AboutCommand>(),
            ],
        };

    [VisualStudioContribution]
    public static MenuConfiguration AddNewMenu
        => new("%AddNewCommand.GroupDisplayName%")
        {
            Placements =
            [
                KnownPlacements.ProjectNode_AddGroup_Submenu_ItemsGroup,
            ],
            Children =
            [
                MenuChild.Group(new CommandGroupConfiguration{
                    Children =
                    [
                        GroupChild.Command<Commands.GenerateAutoRestNewCommand>(),
                        GroupChild.Command<Commands.GenerateRefitterNewCommand>(),
                        GroupChild.Command<Commands.GenerateNSwagNewCommand>(),
                        GroupChild.Command<Commands.GenerateKiotaNewCommand>(),
                        GroupChild.Command<Commands.GenerateOpenApiNewCommand>(),
                        GroupChild.Command<Commands.GenerateSwaggerNewCommand>(),
                    ]
                })
            ],
        };

    protected override void InitializeServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ExtensionSettingsProvider>();
        base.InitializeServices(serviceCollection);
    }

    protected override async Task OnInitializedAsync(
        VisualStudioExtensibility extensibility,
        CancellationToken cancellationToken)
    {
        await base.OnInitializedAsync(extensibility, cancellationToken);

#pragma warning disable VSEXTPREVIEW_OUTPUTWINDOW
        var outputChannel = await extensibility
                    .Views()
                    .Output
                    .CreateOutputChannelAsync(
                        ExtensionName,
                        cancellationToken);
        Logger
            .Setup(new OutputWindowRemoteLogger(outputChannel))
            .WithDefaultTags("VSIX");

#pragma warning disable VSEXTPREVIEW_SETTINGS
        var telemetryOptions = await new ExtensionSettingsProvider(extensibility)
            .GetTelemetryOptionsAsync(cancellationToken);

        if (telemetryOptions.TelemetryOptOut)
        {
            Logger.Instance.Disable();
            return;
        }

        var version = new Version(18, 0); // TODO: Change this to get actual Visual Studio version
        Logger.GetLogger<AppInsightsRemoteLogger>()
            .AddTelemetryInitializer(
                new VisualStudioVersionInitializer(version));
    }
}

internal sealed class VisualStudioVersionInitializer : ITelemetryInitializer
{
    private readonly Version visualStudioVersion;

    public VisualStudioVersionInitializer(Version visualStudioVersion)
    {
        this.visualStudioVersion = visualStudioVersion;
    }

    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry is ISupportProperties supportProperties)
            supportProperties.Properties["visual-studio-version"] = visualStudioVersion.ToString();
    }
}