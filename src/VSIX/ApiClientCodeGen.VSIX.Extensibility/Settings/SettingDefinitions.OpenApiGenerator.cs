#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static partial class SettingDefinitions
{
    [VisualStudioContribution]
    internal static SettingCategory OpenApiGeneratorCategory { get; } = new("openApiGenerator", "%Settings.OpenApi.DisplayName%", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiEmitDefaultValue { get; } = new(
        "emitDefaultValue",
        "%Settings.OpenApi.EmitDefaultValue.DisplayName%",
        OpenApiGeneratorCategory,
        defaultValue: true)
    {
        Description = "%Settings.OpenApi.EmitDefaultValue.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiMethodArgument { get; } = new(
        "methodArgument",
        "%Settings.OpenApi.MethodArgument.DisplayName%",
        OpenApiGeneratorCategory,
        defaultValue: true)
    {
        Description = "%Settings.OpenApi.MethodArgument.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiGeneratePropertyChanged { get; } = new(
        "generatePropertyChanged",
        "%Settings.OpenApi.GeneratePropertyChanged.DisplayName%",
        OpenApiGeneratorCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiUseCollection { get; } = new(
        "useCollection",
        "%Settings.OpenApi.UseCollection.DisplayName%",
        OpenApiGeneratorCategory,
        defaultValue: false)
    {
        Description = "%Settings.OpenApi.UseCollection.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiUseDateTimeOffset { get; } = new(
        "useDateTimeOffset",
        "%Settings.OpenApi.UseDateTimeOffset.DisplayName%",
        OpenApiGeneratorCategory,
        defaultValue: false)
    {
        Description = "%Settings.OpenApi.UseDateTimeOffset.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum OpenApiTargetFramework { get; } = new(
        "targetFramework",
        "%Settings.OpenApi.TargetFramework.DisplayName%",
        OpenApiGeneratorCategory,
        new[]
        {
            new EnumSettingEntry("NetStandard21", "%Settings.OpenApi.TargetFramework.NetStandard21%"),
            new EnumSettingEntry("NetStandard20", "%Settings.OpenApi.TargetFramework.NetStandard20%"),
            new EnumSettingEntry("NetStandard16", "%Settings.OpenApi.TargetFramework.NetStandard16%"),
            new EnumSettingEntry("NetStandard15", "%Settings.OpenApi.TargetFramework.NetStandard15%"),
            new EnumSettingEntry("NetStandard14", "%Settings.OpenApi.TargetFramework.NetStandard14%"),
            new EnumSettingEntry("NetStandard13", "%Settings.OpenApi.TargetFramework.NetStandard13%"),
            new EnumSettingEntry("Net47", "%Settings.OpenApi.TargetFramework.Net47%"),
            new EnumSettingEntry("Net48", "%Settings.OpenApi.TargetFramework.Net48%"),
            new EnumSettingEntry("Net60", "%Settings.OpenApi.TargetFramework.Net60%"),
            new EnumSettingEntry("Net70", "%Settings.OpenApi.TargetFramework.Net70%"),
            new EnumSettingEntry("Net80", "%Settings.OpenApi.TargetFramework.Net80%"),
            new EnumSettingEntry("Net90", "%Settings.OpenApi.TargetFramework.Net90%"),
        },
        defaultValue: "NetStandard21")
    {
        Description = "%Settings.OpenApi.TargetFramework.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String OpenApiCustomAdditionalProperties { get; } = new(
        "customAdditionalProperties",
        "%Settings.OpenApi.CustomAdditionalProperties.DisplayName%",
        OpenApiGeneratorCategory,
        string.Empty)
    {
        Description = "%Settings.OpenApi.CustomAdditionalProperties.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiSkipFormModel { get; } = new(
        "skipFormModel",
        "%Settings.OpenApi.SkipFormModel.DisplayName%",
        OpenApiGeneratorCategory,
        defaultValue: true)
    {
        Description = "%Settings.OpenApi.SkipFormModel.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String OpenApiTemplatesPath { get; } = new(
        "templatesPath",
        "%Settings.OpenApi.TemplatesPath.DisplayName%",
        OpenApiGeneratorCategory,
        string.Empty)
    {
        Description = "%Settings.OpenApi.TemplatesPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiUseConfigurationFile { get; } = new(
        "useConfigurationFile",
        "%Settings.OpenApi.UseConfigurationFile.DisplayName%",
        OpenApiGeneratorCategory,
        defaultValue: true)
    {
        Description = "%Settings.OpenApi.UseConfigurationFile.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiGenerateMultipleFiles { get; } = new(
        "generateMultipleFiles",
        "%Settings.OpenApi.GenerateMultipleFiles.DisplayName%",
        OpenApiGeneratorCategory,
        defaultValue: false)
    {
        Description = "%Settings.OpenApi.GenerateMultipleFiles.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum OpenApiVersion { get; } = new(
        "version",
        "%Settings.OpenApi.Version.DisplayName%",
        OpenApiGeneratorCategory,
        new[]
        {
            new EnumSettingEntry("Latest", "%Settings.OpenApi.Version.Latest%"),
            new EnumSettingEntry("V7180", "%Settings.OpenApi.Version.V7180%"),
            new EnumSettingEntry("V7170", "%Settings.OpenApi.Version.V7170%"),
            new EnumSettingEntry("V7160", "%Settings.OpenApi.Version.V7160%"),
            new EnumSettingEntry("V7150", "%Settings.OpenApi.Version.V7150%"),
            new EnumSettingEntry("V7140", "%Settings.OpenApi.Version.V7140%"),
            new EnumSettingEntry("V7130", "%Settings.OpenApi.Version.V7130%"),
            new EnumSettingEntry("V7120", "%Settings.OpenApi.Version.V7120%"),
            new EnumSettingEntry("V7110", "%Settings.OpenApi.Version.V7110%"),
            new EnumSettingEntry("V7100", "%Settings.OpenApi.Version.V7100%"),
            new EnumSettingEntry("V7090", "%Settings.OpenApi.Version.V7090%"),
            new EnumSettingEntry("V7080", "%Settings.OpenApi.Version.V7080%"),
            new EnumSettingEntry("V7070", "%Settings.OpenApi.Version.V7070%"),
        },
        defaultValue: "Latest")
    {
        Description = "%Settings.OpenApi.Version.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String OpenApiHttpUserAgent { get; } = new(
        "httpUserAgent",
        "%Settings.OpenApi.HttpUserAgent.DisplayName%",
        OpenApiGeneratorCategory,
        string.Empty)
    {
        Description = "%Settings.OpenApi.HttpUserAgent.Description%",
    };
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
