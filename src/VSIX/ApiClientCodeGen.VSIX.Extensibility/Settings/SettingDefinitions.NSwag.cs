#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;
using Rapicgen.Core.Options.NSwag;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static partial class SettingDefinitions
{
    [VisualStudioContribution]
    internal static SettingCategory NSwagCategory { get; } = new("nswag", "%Settings.NSwag.DisplayName%", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagInjectHttpClient { get; } = new(
        "injectHttpClient",
        "%Settings.NSwag.InjectHttpClient.DisplayName%",
        NSwagCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwag.InjectHttpClient.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagGenerateClientInterfaces { get; } = new(
        "generateClientInterfaces",
        "%Settings.NSwag.GenerateClientInterfaces.DisplayName%",
        NSwagCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwag.GenerateClientInterfaces.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagGenerateDtoTypes { get; } = new(
        "generateDtoTypes",
        "%Settings.NSwag.GenerateDtoTypes.DisplayName%",
        NSwagCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwag.GenerateDtoTypes.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagUseBaseUrl { get; } = new(
        "useBaseUrl",
        "%Settings.NSwag.UseBaseUrl.DisplayName%",
        NSwagCategory,
        defaultValue: false)
    {
        Description = "%Settings.NSwag.UseBaseUrl.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum NSwagClassStyle { get; } = new(
        "classStyle",
        "%Settings.NSwag.ClassStyle.DisplayName%",
        NSwagCategory,
        new[]
        {
            new EnumSettingEntry(nameof(CSharpClassStyle.Poco), "%Settings.NSwag.ClassStyle.Poco%"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Inpc), "%Settings.NSwag.ClassStyle.Inpc%"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Prism), "%Settings.NSwag.ClassStyle.Prism%"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Record), "%Settings.NSwag.ClassStyle.Record%"),
        },
        defaultValue: nameof(CSharpClassStyle.Poco))
    {
        Description = "%Settings.NSwag.ClassStyle.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagUseDocumentTitle { get; } = new(
        "useDocumentTitle",
        "%Settings.NSwag.UseDocumentTitle.DisplayName%",
        NSwagCategory,
        defaultValue: true)
    {
        Description = "%Settings.NSwag.UseDocumentTitle.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String NSwagParameterDateTimeFormat { get; } = new(
        "parameterDateTimeFormat",
        "%Settings.NSwag.ParameterDateTimeFormat.DisplayName%",
        NSwagCategory,
        "s")
    {
        Description = "%Settings.NSwag.ParameterDateTimeFormat.Description%",
    };
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
