#pragma warning disable VSEXTPREVIEW_SETTINGS
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;
using Rapicgen.Core.External;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Options.AutoRest;
using Rapicgen.Core.Options.Kiota;
using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace ApiClientCodeGen.VSIX.Extensibility.Settings;

internal static class SettingDefinitions
{
    [VisualStudioContribution]
    internal static SettingCategory RootCategory { get; } = new("restApiClientCodeGenerator", "REST API Client Code Generator");

    [VisualStudioContribution]
    internal static SettingCategory GeneralCategory { get; } = new("general", "General", RootCategory)
    {
        Description = "General settings for REST API Client Code Generator.",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.String JavaPath { get; } = new(
        "javaPath",
        "Java Path",
        GeneralCategory,
        string.Empty)
    {
        Description = "Full path to java.exe. Leave empty to get path from JAVA_HOME",
    };

    [VisualStudioContribution]
    internal static Setting.String NpmPath { get; } = new(
        "npmPath",
        "NPM Path",
        GeneralCategory,
        string.Empty)
    {
        Description = "Full path to npm.cmd",
    };

    [VisualStudioContribution]
    internal static Setting.String NSwagPath { get; } = new(
        "nswagPath",
        "NSwag Path",
        GeneralCategory,
        string.Empty)
    {
        Description = "Full path to NSwag.exe (installs from NPM if not found)",
    };

    [VisualStudioContribution]
    internal static Setting.String SwaggerCodegenPath { get; } = new(
        "swaggerCodegenPath",
        "Swagger Codegen CLI Path",
        GeneralCategory,
        string.Empty)
    {
        Description = "Full path to Swagger Codegen JAR file",
    };

    [VisualStudioContribution]
    internal static Setting.String OpenApiGeneratorPath { get; } = new(
        "openApiGeneratorPath",
        "OpenAPI Generator Path",
        GeneralCategory,
        string.Empty)
    {
        Description = "Full path to OpenAPI Generator JAR file",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean InstallMissingPackages { get; } = new(
        "installMissingPackages",
        "Install Required Packages",
        GeneralCategory,
        defaultValue: true)
    {
        Description = "Automatically install required NuGet packages",
    };

    [VisualStudioContribution]
    internal static SettingCategory AutoRestCategory { get; } = new("autorest", "AutoRest", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestAddCredentials { get; } = new(
        "addCredentials",
        "Add Credentials",
        AutoRestCategory,
        defaultValue: false)
    {
        Description = "Include a credential property and constructor parameter supporting different authentication behaviors",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestOverrideClientName { get; } = new(
        "overrideClientName",
        "Override Client Name",
        AutoRestCategory,
        defaultValue: false)
    {
        Description = "Overrides the name of the client class using the document filename instead of the OpenAPI title",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestUseInternalConstructors { get; } = new(
        "useInternalConstructors",
        "Use Internal Constructors",
        AutoRestCategory,
        defaultValue: false)
    {
        Description = "Generate constructors with internal instead of public visibility",
    };

    [VisualStudioContribution]
    internal static Setting.Enum AutoRestSyncMethods { get; } = new(
        "syncMethods",
        "Generate Sync Methods",
        AutoRestCategory,
        new[]
        {
            new EnumSettingEntry(nameof(SyncMethodOptions.Essential), "Essential"),
            new EnumSettingEntry(nameof(SyncMethodOptions.All), "All"),
            new EnumSettingEntry(nameof(SyncMethodOptions.None), "None"),
        },
        defaultValue: nameof(SyncMethodOptions.Essential))
    {
        Description = "Determines the amount of synchronous wrappers to generate",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestUseDateTimeOffset { get; } = new(
        "useDateTimeOffset",
        "Use DateTimeOffset",
        AutoRestCategory,
        defaultValue: false)
    {
        Description = "Use DateTimeOffset instead of DateTime to model date/time types",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean AutoRestClientSideValidation { get; } = new(
        "clientSideValidation",
        "Client Side Validation",
        AutoRestCategory,
        defaultValue: true)
    {
        Description = "Validate parameters at the client side before making a request",
    };

    [VisualStudioContribution]
    internal static SettingCategory NSwagCategory { get; } = new("nswag", "NSwag", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagInjectHttpClient { get; } = new(
        "injectHttpClient",
        "Inject HttpClient",
        NSwagCategory,
        defaultValue: true)
    {
        Description = "Generate constructors that accept HttpClient",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagGenerateClientInterfaces { get; } = new(
        "generateClientInterfaces",
        "Generate Interfaces",
        NSwagCategory,
        defaultValue: true)
    {
        Description = "Generate client interfaces",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagGenerateDtoTypes { get; } = new(
        "generateDtoTypes",
        "Generate DTO types",
        NSwagCategory,
        defaultValue: true)
    {
        Description = "Generate DTO types",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagUseBaseUrl { get; } = new(
        "useBaseUrl",
        "Use Base URL",
        NSwagCategory,
        defaultValue: false)
    {
        Description = "Include a base URL for every HTTP request",
    };

    [VisualStudioContribution]
    internal static Setting.Enum NSwagClassStyle { get; } = new(
        "classStyle",
        "C# Class Style",
        NSwagCategory,
        new[]
        {
            new EnumSettingEntry(nameof(CSharpClassStyle.Poco), "POCO"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Inpc), "INPC"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Prism), "Prism"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Record), "Record"),
        },
        defaultValue: nameof(CSharpClassStyle.Poco))
    {
        Description = "Select the C# class style for generated code",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagUseDocumentTitle { get; } = new(
        "useDocumentTitle",
        "Document title as class name",
        NSwagCategory,
        defaultValue: true)
    {
        Description = "Use the OpenAPI document title as the generated class name",
    };

    [VisualStudioContribution]
    internal static Setting.String NSwagParameterDateTimeFormat { get; } = new(
        "parameterDateTimeFormat",
        "Parameter DateTime format",
        NSwagCategory,
        "s")
    {
        Description = "Specifies the format for DateTime method parameters",
    };

    [VisualStudioContribution]
    internal static SettingCategory NSwagStudioCategory { get; } = new("nswagStudio", "NSwag Studio", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioInjectHttpClient { get; } = new(
        "injectHttpClient",
        "Inject HttpClient",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "Generate constructors that accept HttpClient",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateClientInterfaces { get; } = new(
        "generateClientInterfaces",
        "Generate Interfaces",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "Generate client interfaces",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateDtoTypes { get; } = new(
        "generateDtoTypes",
        "Generate DTO types",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "Generate DTO types",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioUseBaseUrl { get; } = new(
        "useBaseUrl",
        "Use Base URL",
        NSwagStudioCategory,
        defaultValue: false)
    {
        Description = "Include a base URL for every HTTP request",
    };

    [VisualStudioContribution]
    internal static Setting.Enum NSwagStudioClassStyle { get; } = new(
        "classStyle",
        "C# Class Style",
        NSwagStudioCategory,
        new[]
        {
            new EnumSettingEntry(nameof(CSharpClassStyle.Poco), "POCO"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Inpc), "INPC"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Prism), "Prism"),
            new EnumSettingEntry(nameof(CSharpClassStyle.Record), "Record"),
        },
        defaultValue: nameof(CSharpClassStyle.Poco))
    {
        Description = "Select the C# class style for generated code",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioUseDocumentTitle { get; } = new(
        "useDocumentTitle",
        "Document title as class name",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "Use the OpenAPI document title as the generated class name",
    };

    [VisualStudioContribution]
    internal static Setting.String NSwagStudioParameterDateTimeFormat { get; } = new(
        "parameterDateTimeFormat",
        "Parameter DateTime format",
        NSwagStudioCategory,
        "s")
    {
        Description = "Specifies the format for DateTime method parameters",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateResponseClasses { get; } = new(
        "generateResponseClasses",
        "Generate Response Classes",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "Generate response classes",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateJsonMethods { get; } = new(
        "generateJsonMethods",
        "Generate JSON methods",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "Generate JSON methods",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioRequiredPropertiesMustBeDefined { get; } = new(
        "requiredPropertiesMustBeDefined",
        "Required properties must be defined",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "Require required properties to be defined",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateDefaultValues { get; } = new(
        "generateDefaultValues",
        "Generate Default Values",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "Generate default values",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean NSwagStudioGenerateDataAnnotations { get; } = new(
        "generateDataAnnotations",
        "Generate Data Annotations",
        NSwagStudioCategory,
        defaultValue: true)
    {
        Description = "Generate data annotations",
    };

    [VisualStudioContribution]
    internal static SettingCategory OpenApiGeneratorCategory { get; } = new("openApiGenerator", "OpenAPI Generator", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiEmitDefaultValue { get; } = new(
        "emitDefaultValue",
        "Emit Default Value",
        OpenApiGeneratorCategory,
        defaultValue: true)
    {
        Description = "Generate the default value for members in the serialization stream",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiMethodArgument { get; } = new(
        "methodArgument",
        "Optional Method Arguments",
        OpenApiGeneratorCategory,
        defaultValue: true)
    {
        Description = "Use optional method arguments (C# optional parameters)",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiGeneratePropertyChanged { get; } = new(
        "generatePropertyChanged",
        "Generate Property Changed",
        OpenApiGeneratorCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiUseCollection { get; } = new(
        "useCollection",
        "Use ICollection<T>",
        OpenApiGeneratorCategory,
        defaultValue: false)
    {
        Description = "Deserialize array types to Collection<T> instead of List<T>",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiUseDateTimeOffset { get; } = new(
        "useDateTimeOffset",
        "Use DateTimeOffset",
        OpenApiGeneratorCategory,
        defaultValue: false)
    {
        Description = "Use DateTimeOffset to model date-time properties",
    };

    [VisualStudioContribution]
    internal static Setting.Enum OpenApiTargetFramework { get; } = new(
        "targetFramework",
        "Target Framework",
        OpenApiGeneratorCategory,
        new[]
        {
            new EnumSettingEntry("NetStandard21", "netstandard2.1"),
            new EnumSettingEntry("NetStandard20", "netstandard2.0"),
            new EnumSettingEntry("NetStandard16", "netstandard1.6"),
            new EnumSettingEntry("NetStandard15", "netstandard1.5"),
            new EnumSettingEntry("NetStandard14", "netstandard1.4"),
            new EnumSettingEntry("NetStandard13", "netstandard1.3"),
            new EnumSettingEntry("Net47", "net47"),
            new EnumSettingEntry("Net48", "net48"),
            new EnumSettingEntry("Net60", "net6.0"),
            new EnumSettingEntry("Net70", "net7.0"),
            new EnumSettingEntry("Net80", "net8.0"),
            new EnumSettingEntry("Net90", "net9.0"),
        },
        defaultValue: "NetStandard21")
    {
        Description = "The target .NET framework version",
    };

    [VisualStudioContribution]
    internal static Setting.String OpenApiCustomAdditionalProperties { get; } = new(
        "customAdditionalProperties",
        "Custom Additional Properties",
        OpenApiGeneratorCategory,
        string.Empty)
    {
        Description = "Overrides all other additional properties",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiSkipFormModel { get; } = new(
        "skipFormModel",
        "Skip Form Model",
        OpenApiGeneratorCategory,
        defaultValue: true)
    {
        Description = "Skip models defined as form parameters in requestBody",
    };

    [VisualStudioContribution]
    internal static Setting.String OpenApiTemplatesPath { get; } = new(
        "templatesPath",
        "Templates Path",
        OpenApiGeneratorCategory,
        string.Empty)
    {
        Description = "Path to the folder containing the custom Mustache templates",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiUseConfigurationFile { get; } = new(
        "useConfigurationFile",
        "Use Configuration File",
        OpenApiGeneratorCategory,
        defaultValue: true)
    {
        Description = "Use the configuration file if present",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean OpenApiGenerateMultipleFiles { get; } = new(
        "generateMultipleFiles",
        "Generate Multiple Files",
        OpenApiGeneratorCategory,
        defaultValue: false)
    {
        Description = "Generate multiple files for each operation (SDK style projects only)",
    };

    [VisualStudioContribution]
    internal static Setting.Enum OpenApiVersion { get; } = new(
        "version",
        "Version",
        OpenApiGeneratorCategory,
        new[]
        {
            new EnumSettingEntry("Latest", "Latest"),
            new EnumSettingEntry("V7180", "7.18.0"),
            new EnumSettingEntry("V7170", "7.17.0"),
            new EnumSettingEntry("V7160", "7.16.0"),
            new EnumSettingEntry("V7150", "7.15.0"),
            new EnumSettingEntry("V7140", "7.14.0"),
            new EnumSettingEntry("V7130", "7.13.0"),
            new EnumSettingEntry("V7120", "7.12.0"),
            new EnumSettingEntry("V7110", "7.11.0"),
            new EnumSettingEntry("V7100", "7.10.0"),
            new EnumSettingEntry("V7090", "7.9.0"),
            new EnumSettingEntry("V7080", "7.8.0"),
            new EnumSettingEntry("V7070", "7.7.0"),
        },
        defaultValue: "Latest")
    {
        Description = "The version of the generator to use",
    };

    [VisualStudioContribution]
    internal static Setting.String OpenApiHttpUserAgent { get; } = new(
        "httpUserAgent",
        "HTTP User-Agent",
        OpenApiGeneratorCategory,
        string.Empty)
    {
        Description = "Sets the User-Agent header value to be sent in the HTTP request",
    };

    [VisualStudioContribution]
    internal static SettingCategory RefitterCategory { get; } = new("refitter", "Refitter", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateContracts { get; } = new(
        "generateContracts",
        "Generate Contracts",
        RefitterCategory,
        defaultValue: true)
    {
        Description = "Generate the contract types",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateXmlDocCodeComments { get; } = new(
        "generateXmlDocCodeComments",
        "Generate XML code comments",
        RefitterCategory,
        defaultValue: true)
    {
        Description = "Generate XML documentation style code comments",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterAddAutoGeneratedHeader { get; } = new(
        "addAutoGeneratedHeader",
        "Generate <auto-generated> Header",
        RefitterCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterReturnIApiResponse { get; } = new(
        "returnIApiResponse",
        "Return IApiResponse<T>",
        RefitterCategory,
        defaultValue: false)
    {
        Description = "Wrap returned contract types in IApiResponse<T>",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateInternalTypes { get; } = new(
        "generateInternalTypes",
        "Generate internal types",
        RefitterCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterUseCancellationTokens { get; } = new(
        "useCancellationTokens",
        "Use Cancellation Tokens",
        RefitterCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateHeaderParameters { get; } = new(
        "generateHeaderParameters",
        "Generate Header Parameters",
        RefitterCategory,
        defaultValue: true);

    [VisualStudioContribution]
    internal static Setting.Boolean RefitterGenerateMultipleFiles { get; } = new(
        "generateMultipleFiles",
        "Generate Multiple Files",
        RefitterCategory,
        defaultValue: false);

    [VisualStudioContribution]
    internal static SettingCategory KiotaCategory { get; } = new("kiota", "Kiota", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean KiotaGenerateMultipleFiles { get; } = new(
        "generateMultipleFiles",
        "Generate Multiple Files",
        KiotaCategory,
        defaultValue: false)
    {
        Description = "Generate multiple files for each operation (SDK style projects only)",
    };

    [VisualStudioContribution]
    internal static Setting.Enum KiotaTypeAccessModifier { get; } = new(
        "typeAccessModifier",
        "Type Access Modifier",
        KiotaCategory,
        new[]
        {
            new EnumSettingEntry("Public", "Public"),
            new EnumSettingEntry("Internal", "Internal"),
            new EnumSettingEntry("Protected", "Protected"),
            new EnumSettingEntry("Private", "Private"),
        },
        defaultValue: "Public")
    {
        Description = "The access modifier for the generated types",
    };

    [VisualStudioContribution]
    internal static Setting.Boolean KiotaUsesBackingStore { get; } = new(
        "usesBackingStore",
        "Generate Backing Store",
        KiotaCategory,
        defaultValue: false)
    {
        Description = "Generate persistence code for Entity Framework",
    };

    [VisualStudioContribution]
    internal static SettingCategory AnalyticsCategory { get; } = new("analytics", "Analytics", RootCategory)
    {
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    internal static Setting.Boolean TelemetryOptOut { get; } = new(
        "telemetryOptOut",
        "Opt-out",
        AnalyticsCategory,
        defaultValue: false)
    {
        Description = "Opt-out of telemetry collection",
    };
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
