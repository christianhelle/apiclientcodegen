#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static class KiotaSettings
{
    [VisualStudioContribution]
    internal static SettingCategory KiotaCategory { get; } = new("kiota", "%Settings.Kiota.DisplayName%", SettingsRoot.RootCategory)
    {
        GenerateObserverClass = true,
        Order = 6,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean KiotaGenerateMultipleFiles { get; } = new(
        "generateMultipleFiles",
        "%Settings.Kiota.GenerateMultipleFiles.DisplayName%",
        KiotaCategory,
        defaultValue: false)
    {
        Description = "%Settings.Kiota.GenerateMultipleFiles.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum KiotaTypeAccessModifier { get; } = new(
        "typeAccessModifier",
        "%Settings.Kiota.TypeAccessModifier.DisplayName%",
        KiotaCategory,
        new[]
        {
            new EnumSettingEntry("Public", "%Settings.Kiota.TypeAccessModifier.Public%"),
            new EnumSettingEntry("Internal", "%Settings.Kiota.TypeAccessModifier.Internal%"),
            new EnumSettingEntry("Protected", "%Settings.Kiota.TypeAccessModifier.Protected%"),
            new EnumSettingEntry("Private", "%Settings.Kiota.TypeAccessModifier.Private%"),
        },
        defaultValue: "Public")
    {
        Description = "%Settings.Kiota.TypeAccessModifier.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean KiotaUsesBackingStore { get; } = new(
        "usesBackingStore",
        "%Settings.Kiota.UsesBackingStore.DisplayName%",
        KiotaCategory,
        defaultValue: false)
    {
        Description = "%Settings.Kiota.UsesBackingStore.Description%",
    };
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
