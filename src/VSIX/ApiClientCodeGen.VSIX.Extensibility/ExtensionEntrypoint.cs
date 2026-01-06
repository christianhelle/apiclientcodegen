using ApiClientCodeGen.VSIX.Extensibility.Commands.Placements;
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

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
}
