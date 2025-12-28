using Microsoft.VisualStudio.Extensibility.Commands;

namespace ApiClientCodeGen.VSIX.Extensibility.CommandPlacements;

public static class KnownPlacements
{
    public static CommandPlacement FileInProjectContextMenu
        => CommandPlacement.VsctParent(
            new Guid("{d309f791-903f-11d0-9efc-00a0c911004f}"),
            id: 0x0209,
            priority: 0x0801);

    public static CommandPlacement ProjectContextMenu
        => CommandPlacement.VsctParent(
            new Guid("{d309f791-903f-11d0-9efc-00a0c911004f}"),
            id: 0x0206,
            priority: 0x0801);

    public static CommandPlacement SolutionContextMenu
        => CommandPlacement.VsctParent(
            new Guid("{d309f791-903f-11d0-9efc-00a0c911004f}"),
            id: 0x0219,
            priority: 0x0801);
}