#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static partial class SettingDefinitions
{
    [VisualStudioContribution]
    internal static SettingCategory RootCategory { get; } = new("restApiClientCodeGenerator", "%Settings.Root.DisplayName%");
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
