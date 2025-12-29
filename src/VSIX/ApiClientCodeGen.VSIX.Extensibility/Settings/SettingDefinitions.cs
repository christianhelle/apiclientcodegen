#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;
using Rapicgen.Core.Options.AutoRest;
using Rapicgen.Core.Options.NSwag;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static class SettingDefinitions
{
    [VisualStudioContribution]
    internal static SettingCategory RootCategory { get; } = new("restApiClientCodeGenerator", "%Settings.Root.DisplayName%");

    [VisualStudioContribution]
    internal static SettingCategory GeneralCategory { get; } = new("general", "%Settings.General.DisplayName%", RootCategory)
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

    [VisualStudioContribution]
    internal static SettingCategory AutoRestCategory { get; } = new("autorest", "%Settings.AutoRest.DisplayName%", RootCategory)
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

    [VisualStudioContribution]
    internal static SettingCategory NSwagStudioCategory { get; } = new("nswagStudio", "%Settings.NSwagStudio.DisplayName%", RootCategory)
    {
        GenerateObserverClass = true,
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

    [VisualStudioContribution]
    internal static SettingCategory RefitterCategory { get; } = new("refitter", "%Settings.Refitter.DisplayName%", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateContracts { get; } = new(
        "generateContracts",
        "%Settings.Refitter.GenerateContracts.DisplayName%",
        RefitterCategory,
        defaultValue: true)
    {
        Description = "%Settings.Refitter.GenerateContracts.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateXmlDocCodeComments { get; } = new(
        "generateXmlDocCodeComments",
        "%Settings.Refitter.GenerateXmlDocCodeComments.DisplayName%",
        RefitterCategory,
        defaultValue: true)
    {
        Description = "%Settings.Refitter.GenerateXmlDocCodeComments.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterAddAutoGeneratedHeader { get; } = new(
        "addAutoGeneratedHeader",
        "%Settings.Refitter.AddAutoGeneratedHeader.DisplayName%",
        RefitterCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterReturnIApiResponse { get; } = new(
        "returnIApiResponse",
        "%Settings.Refitter.ReturnIApiResponse.DisplayName%",
        RefitterCategory,
        defaultValue: false)
    {
        Description = "%Settings.Refitter.ReturnIApiResponse.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateInternalTypes { get; } = new(
        "generateInternalTypes",
        "%Settings.Refitter.GenerateInternalTypes.DisplayName%",
        RefitterCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterUseCancellationTokens { get; } = new(
        "useCancellationTokens",
        "%Settings.Refitter.UseCancellationTokens.DisplayName%",
        RefitterCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateHeaderParameters { get; } = new(
        "generateHeaderParameters",
        "%Settings.Refitter.GenerateHeaderParameters.DisplayName%",
        RefitterCategory,
        defaultValue: true);

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateMultipleFiles { get; } = new(
        "generateMultipleFiles",
        "%Settings.Refitter.GenerateMultipleFiles.DisplayName%",
        RefitterCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static SettingCategory KiotaCategory { get; } = new("kiota", "%Settings.Kiota.DisplayName%", RootCategory)
    {
        GenerateObserverClass = true,
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

    [VisualStudioContribution]
    internal static SettingCategory AnalyticsCategory { get; } = new("analytics", "%Settings.Analytics.DisplayName%", RootCategory)
    {
        GenerateObserverClass = true,
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
