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
            id: "APIClientCodeGenerator2026.c28b8f61-bafa-4dc5-a0cc-44b47f3f1c39",
            version: this.ExtensionAssemblyVersion,
            publisherName: "ChristianResmaHelle",
            displayName: "REST API Client Code Generator",
            description: "Generate REST API client code from OpenAPI/Swagger specifications"),
    };

    public static CommandGroupConfiguration MyGroup => new()
    {
        Children = new[]
        {
            GroupChild.Command<Commands.GenerateRefitterCommand>(),
            GroupChild.Command<Commands.GenerateNSwagCommand>(),
            GroupChild.Command<Commands.GenerateOpenApiCommand>(),
            GroupChild.Command<Commands.GenerateKiotaCommand>(),
            GroupChild.Command<Commands.GenerateSwaggerCommand>(),
            GroupChild.Command<Commands.GenerateAutoRestCommand>(),
            GroupChild.Command<Commands.AboutCommand>(),
        }
    };

    [VisualStudioContribution]
    public static MenuConfiguration MyMenu => new("%ApiClientCodeGenerator.GroupDisplayName%")
    {
        Placements =
        [
            KnownPlacements.ItemNode_OpenGroup,
            KnownPlacements.ProjectNode_BuildGroup,
            KnownPlacements.SolutionNode_BuildGroup,
        ],
        Children = [MenuChild.Group(MyGroup)],
    };
}
