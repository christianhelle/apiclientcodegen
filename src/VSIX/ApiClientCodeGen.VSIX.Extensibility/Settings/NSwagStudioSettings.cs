#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;
using Rapicgen.Core.Options.NSwag;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static class NSwagStudioSettings
{
    [VisualStudioContribution]
    internal static SettingCategory NSwagStudioCategory { get; } = new("nswagStudio", "%Settings.NSwagStudio.DisplayName%", SettingsRoot.RootCategory)
    {
        GenerateObserverClass = true,
        Order = 4,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioInjectHttpClient { get; } = new(
        "injectHttpClient",
        "%Settings.NSwagStudio.InjectHttpClient.DisplayName%",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.InjectHttpClient.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateClientInterfaces { get; } = new(
        "generateClientInterfaces",
        "%Settings.NSwagStudio.GenerateClientInterfaces.DisplayName%",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateClientInterfaces.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateDtoTypes { get; } = new(
        "generateDtoTypes",
        "%Settings.NSwagStudio.GenerateDtoTypes.DisplayName%",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateDtoTypes.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioUseBaseUrl { get; } = new(
        "useBaseUrl",
        "%Settings.NSwagStudio.UseBaseUrl.DisplayName%",
        NSwagStudioCategory,
        defaultValue: false)
    {
        Description = "%Settings.NSwagStudio.UseBaseUrl.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum NSwagStudioClassStyle { get; } = new(
        "classStyle",
        "%Settings.NSwagStudio.ClassStyle.DisplayName%",
        NSwagStudioCategory,
        new[]
        {
            new EnumSettingEntry(nameof(CSharpClassStyle.Poco), "%Settings.NSwagStudio.ClassStyle.Poco%"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Inpc), "%Settings.NSwagStudio.ClassStyle.Inpc%"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Prism), "%Settings.NSwagStudio.ClassStyle.Prism%"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Record), "%Settings.NSwagStudio.ClassStyle.Record%"),
        },
        defaultValue: nameof(CSharpClassStyle.Poco))
    {
        Description = "%Settings.NSwagStudio.ClassStyle.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioUseDocumentTitle { get; } = new(
        "useDocumentTitle",
        "%Settings.NSwagStudio.UseDocumentTitle.DisplayName%",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.UseDocumentTitle.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String NSwagStudioParameterDateTimeFormat { get; } = new(
        "parameterDateTimeFormat",
        "%Settings.NSwagStudio.ParameterDateTimeFormat.DisplayName%",
        NSwagStudioCategory,
        "s")
    {
        Description = "%Settings.NSwagStudio.ParameterDateTimeFormat.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateResponseClasses { get; } = new(
        "generateResponseClasses",
        "%Settings.NSwagStudio.GenerateResponseClasses.DisplayName%",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateResponseClasses.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateJsonMethods { get; } = new(
        "generateJsonMethods",
        "%Settings.NSwagStudio.GenerateJsonMethods.DisplayName%",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateJsonMethods.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioRequiredPropertiesMustBeDefined { get; } = new(
        "requiredPropertiesMustBeDefined",
        "%Settings.NSwagStudio.RequiredProperties.DisplayName%",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.RequiredProperties.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateDefaultValues { get; } = new(
        "generateDefaultValues",
        "%Settings.NSwagStudio.GenerateDefaultValues.DisplayName%",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateDefaultValues.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateDataAnnotations { get; } = new(
        "generateDataAnnotations",
        "%Settings.NSwagStudio.GenerateDataAnnotations.DisplayName%",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateDataAnnotations.Description%",
    };
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
