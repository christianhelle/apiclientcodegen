#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static class GeneralSettings
{
    [VisualStudioContribution]
    internal static SettingCategory GeneralCategory { get; } = new("general", "%Settings.General.DisplayName%", SettingsRoot.RootCategory)
    {
        Description = "%Settings.General.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.String JavaPath { get; } = new(
        "javaPath",
        "%Settings.JavaPath.DisplayName%",
        GeneralCategory,
        string.Empty)
    {
        Description = "%Settings.JavaPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String NpmPath { get; } = new(
        "npmPath",
        "%Settings.NpmPath.DisplayName%",
        GeneralCategory,
        string.Empty)
    {
        Description = "%Settings.NpmPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String NSwagPath { get; } = new(
        "nswagPath",
        "%Settings.NSwagPath.DisplayName%",
        GeneralCategory,
        string.Empty)
    {
        Description = "%Settings.NSwagPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String SwaggerCodegenPath { get; } = new(
        "swaggerCodegenPath",
        "%Settings.SwaggerCodegenPath.DisplayName%",
        GeneralCategory,
        string.Empty)
    {
        Description = "%Settings.SwaggerCodegenPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String OpenApiGeneratorPath { get; } = new(
        "openApiGeneratorPath",
        "%Settings.OpenApiGeneratorPath.DisplayName%",
        GeneralCategory,
        string.Empty)
    {
        Description = "%Settings.OpenApiGeneratorPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean InstallMissingPackages { get; } = new(
        "installMissingPackages",
        "%Settings.InstallMissingPackages.DisplayName%",
        GeneralCategory,
        defaultValue: true)
    {
        Description = "%Settings.InstallMissingPackages.Description%",
    };
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
