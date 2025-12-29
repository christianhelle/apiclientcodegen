namespace ApiClientCodeGen.VSIX.Extensibility;

using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;

#pragma warning disable VSEXTPREVIEW_SETTINGS // The settings API is currently in preview and marked as experimental

internal static class SettingDefinitions
{
    // General Settings Category
    [VisualStudioContribution]
    internal static SettingCategory GeneralCategory { get; } = new("general", "%Settings.General.Category.DisplayName%")
    {
        Description = "%Settings.General.Category.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.String JavaPathSetting { get; } = new("javaPath", "%Settings.General.JavaPath.DisplayName%", GeneralCategory, defaultValue: string.Empty)
    {
        Description = "%Settings.General.JavaPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String NpmPathSetting { get; } = new("npmPath", "%Settings.General.NpmPath.DisplayName%", GeneralCategory, defaultValue: string.Empty)
    {
        Description = "%Settings.General.NpmPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String NSwagPathSetting { get; } = new("nswagPath", "%Settings.General.NSwagPath.DisplayName%", GeneralCategory, defaultValue: string.Empty)
    {
        Description = "%Settings.General.NSwagPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String SwaggerCodegenPathSetting { get; } = new("swaggerCodegenPath", "%Settings.General.SwaggerCodegenPath.DisplayName%", GeneralCategory, defaultValue: string.Empty)
    {
        Description = "%Settings.General.SwaggerCodegenPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String OpenApiGeneratorPathSetting { get; } = new("openApiGeneratorPath", "%Settings.General.OpenApiGeneratorPath.DisplayName%", GeneralCategory, defaultValue: string.Empty)
    {
        Description = "%Settings.General.OpenApiGeneratorPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean InstallMissingPackagesSetting { get; } = new("installMissingPackages", "%Settings.General.InstallMissingPackages.DisplayName%", GeneralCategory, defaultValue: true)
    {
        Description = "%Settings.General.InstallMissingPackages.Description%",
    };

    // Analytics Settings Category
    [VisualStudioContribution]
    internal static SettingCategory AnalyticsCategory { get; } = new("analytics", "%Settings.Analytics.Category.DisplayName%")
    {
        Description = "%Settings.Analytics.Category.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean TelemetryOptOutSetting { get; } = new("telemetryOptOut", "%Settings.Analytics.TelemetryOptOut.DisplayName%", AnalyticsCategory, defaultValue: false)
    {
        Description = "%Settings.Analytics.TelemetryOptOut.Description%",
    };

    // AutoRest Settings Category
    [VisualStudioContribution]
    internal static SettingCategory AutoRestCategory { get; } = new("autoRest", "%Settings.AutoRest.Category.DisplayName%")
    {
        Description = "%Settings.AutoRest.Category.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AddCredentialsSetting { get; } = new("addCredentials", "%Settings.AutoRest.AddCredentials.DisplayName%", AutoRestCategory, defaultValue: false)
    {
        Description = "%Settings.AutoRest.AddCredentials.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OverrideClientNameSetting { get; } = new("overrideClientName", "%Settings.AutoRest.OverrideClientName.DisplayName%", AutoRestCategory, defaultValue: false)
    {
        Description = "%Settings.AutoRest.OverrideClientName.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseInternalConstructorsSetting { get; } = new("useInternalConstructors", "%Settings.AutoRest.UseInternalConstructors.DisplayName%", AutoRestCategory, defaultValue: false)
    {
        Description = "%Settings.AutoRest.UseInternalConstructors.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum SyncMethodsSetting { get; } = new(
        "syncMethods",
        "%Settings.AutoRest.SyncMethods.DisplayName%",
        AutoRestCategory,
        [
            new EnumSettingEntry("Essential", "%Settings.AutoRest.SyncMethods.Essential%"),
            new EnumSettingEntry("All", "%Settings.AutoRest.SyncMethods.All%"),
            new EnumSettingEntry("None", "%Settings.AutoRest.SyncMethods.None%")
        ],
        defaultValue: "Essential")
    {
        Description = "%Settings.AutoRest.SyncMethods.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseDateTimeOffsetAutoRestSetting { get; } = new("useDateTimeOffset", "%Settings.AutoRest.UseDateTimeOffset.DisplayName%", AutoRestCategory, defaultValue: false)
    {
        Description = "%Settings.AutoRest.UseDateTimeOffset.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean ClientSideValidationSetting { get; } = new("clientSideValidation", "%Settings.AutoRest.ClientSideValidation.DisplayName%", AutoRestCategory, defaultValue: true)
    {
        Description = "%Settings.AutoRest.ClientSideValidation.Description%",
    };

    // NSwag Settings Category
    [VisualStudioContribution]
    internal static SettingCategory NSwagCategory { get; } = new("nswag", "%Settings.NSwag.Category.DisplayName%")
    {
        Description = "%Settings.NSwag.Category.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean InjectHttpClientSetting { get; } = new("injectHttpClient", "%Settings.NSwag.InjectHttpClient.DisplayName%", NSwagCategory, defaultValue: true)
    {
        Description = "%Settings.NSwag.InjectHttpClient.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateClientInterfacesSetting { get; } = new("generateClientInterfaces", "%Settings.NSwag.GenerateClientInterfaces.DisplayName%", NSwagCategory, defaultValue: true)
    {
        Description = "%Settings.NSwag.GenerateClientInterfaces.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateDtoTypesSetting { get; } = new("generateDtoTypes", "%Settings.NSwag.GenerateDtoTypes.DisplayName%", NSwagCategory, defaultValue: true)
    {
        Description = "%Settings.NSwag.GenerateDtoTypes.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseBaseUrlSetting { get; } = new("useBaseUrl", "%Settings.NSwag.UseBaseUrl.DisplayName%", NSwagCategory, defaultValue: false)
    {
        Description = "%Settings.NSwag.UseBaseUrl.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum ClassStyleSetting { get; } = new(
        "classStyle",
        "%Settings.NSwag.ClassStyle.DisplayName%",
        NSwagCategory,
        [
            new EnumSettingEntry("Poco", "%Settings.NSwag.ClassStyle.Poco%"),
            new EnumSettingEntry("Inpc", "%Settings.NSwag.ClassStyle.Inpc%"),
            new EnumSettingEntry("Prism", "%Settings.NSwag.ClassStyle.Prism%"),
            new EnumSettingEntry("Record", "%Settings.NSwag.ClassStyle.Record%")
        ],
        defaultValue: "Poco")
    {
        Description = "%Settings.NSwag.ClassStyle.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseDocumentTitleSetting { get; } = new("useDocumentTitle", "%Settings.NSwag.UseDocumentTitle.DisplayName%", NSwagCategory, defaultValue: true)
    {
        Description = "%Settings.NSwag.UseDocumentTitle.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String ParameterDateTimeFormatSetting { get; } = new("parameterDateTimeFormat", "%Settings.NSwag.ParameterDateTimeFormat.DisplayName%", NSwagCategory, defaultValue: "s")
    {
        Description = "%Settings.NSwag.ParameterDateTimeFormat.Description%",
    };

    // NSwag Studio Settings Category
    [VisualStudioContribution]
    internal static SettingCategory NSwagStudioCategory { get; } = new("nswagStudio", "%Settings.NSwagStudio.Category.DisplayName%")
    {
        Description = "%Settings.NSwagStudio.Category.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean InjectHttpClientStudioSetting { get; } = new("injectHttpClient", "%Settings.NSwagStudio.InjectHttpClient.DisplayName%", NSwagStudioCategory, defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.InjectHttpClient.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateClientInterfacesStudioSetting { get; } = new("generateClientInterfaces", "%Settings.NSwagStudio.GenerateClientInterfaces.DisplayName%", NSwagStudioCategory, defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateClientInterfaces.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateDtoTypesStudioSetting { get; } = new("generateDtoTypes", "%Settings.NSwagStudio.GenerateDtoTypes.DisplayName%", NSwagStudioCategory, defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateDtoTypes.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseBaseUrlStudioSetting { get; } = new("useBaseUrl", "%Settings.NSwagStudio.UseBaseUrl.DisplayName%", NSwagStudioCategory, defaultValue: false)
    {
        Description = "%Settings.NSwagStudio.UseBaseUrl.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum ClassStyleStudioSetting { get; } = new(
        "classStyle",
        "%Settings.NSwagStudio.ClassStyle.DisplayName%",
        NSwagStudioCategory,
        [
            new EnumSettingEntry("Poco", "%Settings.NSwagStudio.ClassStyle.Poco%"),
            new EnumSettingEntry("Inpc", "%Settings.NSwagStudio.ClassStyle.Inpc%"),
            new EnumSettingEntry("Prism", "%Settings.NSwagStudio.ClassStyle.Prism%"),
            new EnumSettingEntry("Record", "%Settings.NSwagStudio.ClassStyle.Record%")
        ],
        defaultValue: "Poco")
    {
        Description = "%Settings.NSwagStudio.ClassStyle.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseDocumentTitleStudioSetting { get; } = new("useDocumentTitle", "%Settings.NSwagStudio.UseDocumentTitle.DisplayName%", NSwagStudioCategory, defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.UseDocumentTitle.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String ParameterDateTimeFormatStudioSetting { get; } = new("parameterDateTimeFormat", "%Settings.NSwagStudio.ParameterDateTimeFormat.DisplayName%", NSwagStudioCategory, defaultValue: "s")
    {
        Description = "%Settings.NSwagStudio.ParameterDateTimeFormat.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateResponseClassesSetting { get; } = new("generateResponseClasses", "%Settings.NSwagStudio.GenerateResponseClasses.DisplayName%", NSwagStudioCategory, defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateResponseClasses.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateJsonMethodsSetting { get; } = new("generateJsonMethods", "%Settings.NSwagStudio.GenerateJsonMethods.DisplayName%", NSwagStudioCategory, defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateJsonMethods.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean RequiredPropertiesMustBeDefinedSetting { get; } = new("requiredPropertiesMustBeDefined", "%Settings.NSwagStudio.RequiredPropertiesMustBeDefined.DisplayName%", NSwagStudioCategory, defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.RequiredPropertiesMustBeDefined.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateDefaultValuesSetting { get; } = new("generateDefaultValues", "%Settings.NSwagStudio.GenerateDefaultValues.DisplayName%", NSwagStudioCategory, defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateDefaultValues.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateDataAnnotationsSetting { get; } = new("generateDataAnnotations", "%Settings.NSwagStudio.GenerateDataAnnotations.DisplayName%", NSwagStudioCategory, defaultValue: true)
    {
        Description = "%Settings.NSwagStudio.GenerateDataAnnotations.Description%",
    };

    // OpenAPI Generator Settings Category
    [VisualStudioContribution]
    internal static SettingCategory OpenApiGeneratorCategory { get; } = new("openApiGenerator", "%Settings.OpenApiGenerator.Category.DisplayName%")
    {
        Description = "%Settings.OpenApiGenerator.Category.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean EmitDefaultValueSetting { get; } = new("emitDefaultValue", "%Settings.OpenApiGenerator.EmitDefaultValue.DisplayName%", OpenApiGeneratorCategory, defaultValue: true)
    {
        Description = "%Settings.OpenApiGenerator.EmitDefaultValue.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean MethodArgumentSetting { get; } = new("methodArgument", "%Settings.OpenApiGenerator.MethodArgument.DisplayName%", OpenApiGeneratorCategory, defaultValue: true)
    {
        Description = "%Settings.OpenApiGenerator.MethodArgument.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GeneratePropertyChangedSetting { get; } = new("generatePropertyChanged", "%Settings.OpenApiGenerator.GeneratePropertyChanged.DisplayName%", OpenApiGeneratorCategory, defaultValue: false)
    {
        Description = "%Settings.OpenApiGenerator.GeneratePropertyChanged.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseCollectionSetting { get; } = new("useCollection", "%Settings.OpenApiGenerator.UseCollection.DisplayName%", OpenApiGeneratorCategory, defaultValue: false)
    {
        Description = "%Settings.OpenApiGenerator.UseCollection.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseDateTimeOffsetOpenApiSetting { get; } = new("useDateTimeOffset", "%Settings.OpenApiGenerator.UseDateTimeOffset.DisplayName%", OpenApiGeneratorCategory, defaultValue: false)
    {
        Description = "%Settings.OpenApiGenerator.UseDateTimeOffset.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum TargetFrameworkSetting { get; } = new(
        "targetFramework",
        "%Settings.OpenApiGenerator.TargetFramework.DisplayName%",
        OpenApiGeneratorCategory,
        [
            new EnumSettingEntry("NetStandard21", "%Settings.OpenApiGenerator.TargetFramework.NetStandard21%"),
            new EnumSettingEntry("NetStandard20", "%Settings.OpenApiGenerator.TargetFramework.NetStandard20%"),
            new EnumSettingEntry("NetStandard16", "%Settings.OpenApiGenerator.TargetFramework.NetStandard16%"),
            new EnumSettingEntry("NetStandard15", "%Settings.OpenApiGenerator.TargetFramework.NetStandard15%"),
            new EnumSettingEntry("NetStandard14", "%Settings.OpenApiGenerator.TargetFramework.NetStandard14%"),
            new EnumSettingEntry("NetStandard13", "%Settings.OpenApiGenerator.TargetFramework.NetStandard13%"),
            new EnumSettingEntry("Net47", "%Settings.OpenApiGenerator.TargetFramework.Net47%"),
            new EnumSettingEntry("Net48", "%Settings.OpenApiGenerator.TargetFramework.Net48%"),
            new EnumSettingEntry("Net60", "%Settings.OpenApiGenerator.TargetFramework.Net60%"),
            new EnumSettingEntry("Net70", "%Settings.OpenApiGenerator.TargetFramework.Net70%"),
            new EnumSettingEntry("Net80", "%Settings.OpenApiGenerator.TargetFramework.Net80%"),
            new EnumSettingEntry("Net90", "%Settings.OpenApiGenerator.TargetFramework.Net90%")
        ],
        defaultValue: "NetStandard21")
    {
        Description = "%Settings.OpenApiGenerator.TargetFramework.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String CustomAdditionalPropertiesSetting { get; } = new("customAdditionalProperties", "%Settings.OpenApiGenerator.CustomAdditionalProperties.DisplayName%", OpenApiGeneratorCategory, defaultValue: string.Empty)
    {
        Description = "%Settings.OpenApiGenerator.CustomAdditionalProperties.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean SkipFormModelSetting { get; } = new("skipFormModel", "%Settings.OpenApiGenerator.SkipFormModel.DisplayName%", OpenApiGeneratorCategory, defaultValue: true)
    {
        Description = "%Settings.OpenApiGenerator.SkipFormModel.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String TemplatesPathSetting { get; } = new("templatesPath", "%Settings.OpenApiGenerator.TemplatesPath.DisplayName%", OpenApiGeneratorCategory, defaultValue: string.Empty)
    {
        Description = "%Settings.OpenApiGenerator.TemplatesPath.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseConfigurationFileSetting { get; } = new("useConfigurationFile", "%Settings.OpenApiGenerator.UseConfigurationFile.DisplayName%", OpenApiGeneratorCategory, defaultValue: true)
    {
        Description = "%Settings.OpenApiGenerator.UseConfigurationFile.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateMultipleFilesOpenApiSetting { get; } = new("generateMultipleFiles", "%Settings.OpenApiGenerator.GenerateMultipleFiles.DisplayName%", OpenApiGeneratorCategory, defaultValue: false)
    {
        Description = "%Settings.OpenApiGenerator.GenerateMultipleFiles.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum VersionSetting { get; } = new(
        "version",
        "%Settings.OpenApiGenerator.Version.DisplayName%",
        OpenApiGeneratorCategory,
        [
            new EnumSettingEntry("Latest", "%Settings.OpenApiGenerator.Version.Latest%"),
            new EnumSettingEntry("V7180", "%Settings.OpenApiGenerator.Version.V7180%"),
            new EnumSettingEntry("V7170", "%Settings.OpenApiGenerator.Version.V7170%"),
            new EnumSettingEntry("V7160", "%Settings.OpenApiGenerator.Version.V7160%"),
            new EnumSettingEntry("V7150", "%Settings.OpenApiGenerator.Version.V7150%"),
            new EnumSettingEntry("V7140", "%Settings.OpenApiGenerator.Version.V7140%"),
            new EnumSettingEntry("V7130", "%Settings.OpenApiGenerator.Version.V7130%"),
            new EnumSettingEntry("V7120", "%Settings.OpenApiGenerator.Version.V7120%"),
            new EnumSettingEntry("V7110", "%Settings.OpenApiGenerator.Version.V7110%"),
            new EnumSettingEntry("V7100", "%Settings.OpenApiGenerator.Version.V7100%"),
            new EnumSettingEntry("V7090", "%Settings.OpenApiGenerator.Version.V7090%"),
            new EnumSettingEntry("V7080", "%Settings.OpenApiGenerator.Version.V7080%"),
            new EnumSettingEntry("V7070", "%Settings.OpenApiGenerator.Version.V7070%")
        ],
        defaultValue: "Latest")
    {
        Description = "%Settings.OpenApiGenerator.Version.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.String HttpUserAgentSetting { get; } = new("httpUserAgent", "%Settings.OpenApiGenerator.HttpUserAgent.DisplayName%", OpenApiGeneratorCategory, defaultValue: string.Empty)
    {
        Description = "%Settings.OpenApiGenerator.HttpUserAgent.Description%",
    };

    // Refitter Settings Category
    [VisualStudioContribution]
    internal static SettingCategory RefitterCategory { get; } = new("refitter", "%Settings.Refitter.Category.DisplayName%")
    {
        Description = "%Settings.Refitter.Category.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateContractsSetting { get; } = new("generateContracts", "%Settings.Refitter.GenerateContracts.DisplayName%", RefitterCategory, defaultValue: true)
    {
        Description = "%Settings.Refitter.GenerateContracts.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateXmlDocCodeCommentsSetting { get; } = new("generateXmlDocCodeComments", "%Settings.Refitter.GenerateXmlDocCodeComments.DisplayName%", RefitterCategory, defaultValue: true)
    {
        Description = "%Settings.Refitter.GenerateXmlDocCodeComments.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AddAutoGeneratedHeaderSetting { get; } = new("addAutoGeneratedHeader", "%Settings.Refitter.AddAutoGeneratedHeader.DisplayName%", RefitterCategory, defaultValue: false)
    {
        Description = "%Settings.Refitter.AddAutoGeneratedHeader.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean ReturnIApiResponseSetting { get; } = new("returnIApiResponse", "%Settings.Refitter.ReturnIApiResponse.DisplayName%", RefitterCategory, defaultValue: false)
    {
        Description = "%Settings.Refitter.ReturnIApiResponse.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateInternalTypesSetting { get; } = new("generateInternalTypes", "%Settings.Refitter.GenerateInternalTypes.DisplayName%", RefitterCategory, defaultValue: false)
    {
        Description = "%Settings.Refitter.GenerateInternalTypes.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UseCancellationTokensSetting { get; } = new("useCancellationTokens", "%Settings.Refitter.UseCancellationTokens.DisplayName%", RefitterCategory, defaultValue: false)
    {
        Description = "%Settings.Refitter.UseCancellationTokens.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateHeaderParametersSetting { get; } = new("generateHeaderParameters", "%Settings.Refitter.GenerateHeaderParameters.DisplayName%", RefitterCategory, defaultValue: true)
    {
        Description = "%Settings.Refitter.GenerateHeaderParameters.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateMultipleFilesRefitterSetting { get; } = new("generateMultipleFiles", "%Settings.Refitter.GenerateMultipleFiles.DisplayName%", RefitterCategory, defaultValue: false)
    {
        Description = "%Settings.Refitter.GenerateMultipleFiles.Description%",
    };

    // Kiota Settings Category
    [VisualStudioContribution]
    internal static SettingCategory KiotaCategory { get; } = new("kiota", "%Settings.Kiota.Category.DisplayName%")
    {
        Description = "%Settings.Kiota.Category.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean GenerateMultipleFilesKiotaSetting { get; } = new("generateMultipleFiles", "%Settings.Kiota.GenerateMultipleFiles.DisplayName%", KiotaCategory, defaultValue: false)
    {
        Description = "%Settings.Kiota.GenerateMultipleFiles.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Enum TypeAccessModifierSetting { get; } = new(
        "typeAccessModifier",
        "%Settings.Kiota.TypeAccessModifier.DisplayName%",
        KiotaCategory,
        [
            new EnumSettingEntry("Public", "%Settings.Kiota.TypeAccessModifier.Public%"),
            new EnumSettingEntry("Internal", "%Settings.Kiota.TypeAccessModifier.Internal%"),
            new EnumSettingEntry("Private", "%Settings.Kiota.TypeAccessModifier.Private%"),
            new EnumSettingEntry("Protected", "%Settings.Kiota.TypeAccessModifier.Protected%")
        ],
        defaultValue: "Public")
    {
        Description = "%Settings.Kiota.TypeAccessModifier.Description%",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean UsesBackingStoreSetting { get; } = new("usesBackingStore", "%Settings.Kiota.UsesBackingStore.DisplayName%", KiotaCategory, defaultValue: false)
    {
        Description = "%Settings.Kiota.UsesBackingStore.Description%",
    };
}
