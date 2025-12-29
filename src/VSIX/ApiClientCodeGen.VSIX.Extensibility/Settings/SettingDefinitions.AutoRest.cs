#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;
using Rapicgen.Core.Options.AutoRest;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static class AutoRestSettings
{
    [VisualStudioContribution]
    internal static SettingCategory AutoRestCategory { get; } = new("autorest", "%Settings.AutoRest.DisplayName%", SettingsRoot.RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestAddCredentials { get; } = new(
        "addCredentials",
        "%Settings.AutoRest.AddCredentials.DisplayName%",
        AutoRestCategory,
        defaultValue: false)
    {
        Description = "%Settings.AutoRest.AddCredentials.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestOverrideClientName { get; } = new(
        "overrideClientName",
        "%Settings.AutoRest.OverrideClientName.DisplayName%",
        AutoRestCategory,
        defaultValue: false)
    {
        Description = "%Settings.AutoRest.OverrideClientName.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestUseInternalConstructors { get; } = new(
        "useInternalConstructors",
        "%Settings.AutoRest.UseInternalConstructors.DisplayName%",
        AutoRestCategory,
        defaultValue: false)
    {
        Description = "%Settings.AutoRest.UseInternalConstructors.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum AutoRestSyncMethods { get; } = new(
        "syncMethods",
        "%Settings.AutoRest.SyncMethods.DisplayName%",
        AutoRestCategory,
        new[]
        {
            new EnumSettingEntry(nameof(SyncMethodOptions.Essential), "%Settings.AutoRest.SyncMethods.Essential%"),
            new EnumSettingEntry(nameof(SyncMethodOptions.All), "%Settings.AutoRest.SyncMethods.All%"),
            new EnumSettingEntry(nameof(SyncMethodOptions.None), "%Settings.AutoRest.SyncMethods.None%"),
        },
        defaultValue: nameof(SyncMethodOptions.Essential))
    {
        Description = "%Settings.AutoRest.SyncMethods.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestUseDateTimeOffset { get; } = new(
        "useDateTimeOffset",
        "%Settings.AutoRest.UseDateTimeOffset.DisplayName%",
        AutoRestCategory,
        defaultValue: false)
    {
        Description = "%Settings.AutoRest.UseDateTimeOffset.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestClientSideValidation { get; } = new(
        "clientSideValidation",
        "%Settings.AutoRest.ClientSideValidation.DisplayName%",
        AutoRestCategory,
        defaultValue: true)
    {
        Description = "%Settings.AutoRest.ClientSideValidation.Description%",
    };
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
