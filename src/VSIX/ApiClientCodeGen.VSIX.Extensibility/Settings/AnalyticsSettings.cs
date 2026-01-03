#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static class AnalyticsSettings
{
    [VisualStudioContribution]
    internal static SettingCategory AnalyticsCategory { get; } = new("analytics", "%Settings.Analytics.DisplayName%", SettingsRoot.RootCategory)
    {
        GenerateObserverClass = true,
        Order = 1,
        Description = "%Settings.Analytics.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean TelemetryOptOut { get; } = new(
        "telemetryOptOut",
        "%Settings.TelemetryOptOut.DisplayName%",
        AnalyticsCategory,
        defaultValue: false)
    {
        Description = "%Settings.TelemetryOptOut.Description%",
    };
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
