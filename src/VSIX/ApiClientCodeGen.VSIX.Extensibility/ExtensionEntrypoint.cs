using ApiClientCodeGen.VSIX.Extensibility.Commands.Placements;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace ApiClientCodeGen.VSIX.Extensibility;

[VisualStudioContribution]
internal class ExtensionEntrypoint : Extension
{
    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new(
            id: "f7530eb1-1ce9-46ac-8fab-165b68cf3d61",
            version: ExtensionAssemblyVersion,
            publisherName: "Christian Resma Helle",
            displayName: "REST API Client Code Generator (PREVIEW)",
            description: "Generate REST API client code from OpenAPI/Swagger specifications"),        
    };

    [VisualStudioContribution]
    public static MenuConfiguration GenerateMenu
        => new("%ApiClientCodeGenerator.GroupDisplayName%")
        {
            Placements =
            [
                KnownPlacements.ItemNode_OpenGroup,
                KnownPlacements.ProjectNode_BuildGroup,
            ],
            Children =
            [
                MenuChild.Group(new CommandGroupConfiguration()
                {
                    Children =
                    [
                        GroupChild.Command<Commands.GenerateRefitterCommand>(),
                        GroupChild.Command<Commands.GenerateRefitterSettingsCommand>(),
                        GroupChild.Command<Commands.GenerateNSwagCommand>(),
                        GroupChild.Command<Commands.GenerateNSwagStudioCommand>(),
                        GroupChild.Command<Commands.GenerateOpenApiCommand>(),
                        GroupChild.Command<Commands.GenerateKiotaCommand>(),
                        GroupChild.Command<Commands.GenerateSwaggerCommand>(),
                        GroupChild.Command<Commands.GenerateAutoRestCommand>(),
                        GroupChild.Command<Commands.AboutCommand>(),
                    ]
                })
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
}
