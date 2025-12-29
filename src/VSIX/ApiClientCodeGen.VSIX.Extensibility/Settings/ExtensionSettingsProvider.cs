#pragma warning disable VSEXTPREVIEW_SETTINGS
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using Microsoft.VisualStudio.Extensibility;
using Rapicgen.Core.External;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.AutoRest;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.Kiota;
using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.NSwagStudio;
using Rapicgen.Core.Options.OpenApiGenerator;
using Rapicgen.Core.Options.Refitter;

namespace ApiClientCodeGen.VSIX.Extensibility;

internal class ExtensionSettingsProvider(
    VisualStudioExtensibility extensibility,
    GeneralCategoryObserver generalObserver,
    AutoRestCategoryObserver autoRestObserver,
    NSwagCategoryObserver nswagObserver,
    NSwagStudioCategoryObserver nswagStudioObserver,
    OpenApiGeneratorCategoryObserver openApiObserver,
    RefitterCategoryObserver refitterObserver,
    KiotaCategoryObserver kiotaObserver,
    AnalyticsCategoryObserver analyticsObserver)
{
    public async Task<IGeneralOptions> GetGeneralOptionsAsync(CancellationToken cancellationToken)
        => new GeneralOptions(await generalObserver.GetSnapshotAsync(cancellationToken));

    public async Task<IAutoRestOptions> GetAutoRestOptionsAsync(CancellationToken cancellationToken)
        => new AutoRestOptions(await autoRestObserver.GetSnapshotAsync(cancellationToken));

    public async Task<INSwagOptions> GetNSwagOptionsAsync(CancellationToken cancellationToken)
        => new NSwagOptions(await nswagObserver.GetSnapshotAsync(cancellationToken));

    public async Task<INSwagStudioOptions> GetNSwagStudioOptionsAsync(CancellationToken cancellationToken)
        => new NSwagStudioOptions(await nswagStudioObserver.GetSnapshotAsync(cancellationToken));

    public async Task<IOpenApiGeneratorOptions> GetOpenApiGeneratorOptionsAsync(CancellationToken cancellationToken)
        => new OpenApiOptions(await openApiObserver.GetSnapshotAsync(cancellationToken));

    public async Task<IRefitterOptions> GetRefitterOptionsAsync(CancellationToken cancellationToken)
        => new RefitterOptions(await refitterObserver.GetSnapshotAsync(cancellationToken));

    public async Task<IKiotaOptions> GetKiotaOptionsAsync(CancellationToken cancellationToken)
        => new KiotaOptions(await kiotaObserver.GetSnapshotAsync(cancellationToken));

    public async Task<ITelemetryOptions> GetTelemetryOptionsAsync(CancellationToken cancellationToken)
        => new TelemetryOptions(await analyticsObserver.GetSnapshotAsync(cancellationToken), extensibility);

    private sealed class GeneralOptions(GeneralCategorySnapshot snapshot) : IGeneralOptions
    {
        public string JavaPath => snapshot.JavaPath.ValueOrDefault(PathProvider.GetInstalledJavaPath()) ?? string.Empty;
        public string NpmPath => snapshot.NpmPath.ValueOrDefault(PathProvider.GetNpmPath()) ?? string.Empty;
        public string NSwagPath => snapshot.NSwagPath.ValueOrDefault(PathProvider.GetNSwagStudioPath()) ?? string.Empty;
        public string SwaggerCodegenPath => snapshot.SwaggerCodegenPath.ValueOrDefault(PathProvider.GetSwaggerCodegenPath()) ?? string.Empty;
        public string OpenApiGeneratorPath => snapshot.OpenApiGeneratorPath.ValueOrDefault(PathProvider.GetOpenApiGeneratorPath()) ?? string.Empty;
        public bool? InstallMissingPackages => snapshot.InstallMissingPackages.ValueOrDefault(defaultValue: true);
    }

    private sealed class AutoRestOptions(AutoRestCategorySnapshot snapshot) : IAutoRestOptions
    {
        public bool AddCredentials => snapshot.AutoRestAddCredentials.ValueOrDefault(defaultValue: false);
        public bool OverrideClientName => snapshot.AutoRestOverrideClientName.ValueOrDefault(defaultValue: false);
        public bool UseInternalConstructors => snapshot.AutoRestUseInternalConstructors.ValueOrDefault(defaultValue: false);
        public SyncMethodOptions SyncMethods => ParseEnum(snapshot.AutoRestSyncMethods, SyncMethodOptions.Essential);
        public bool UseDateTimeOffset => snapshot.AutoRestUseDateTimeOffset.ValueOrDefault(defaultValue: false);
        public bool ClientSideValidation => snapshot.AutoRestClientSideValidation.ValueOrDefault(defaultValue: true);
    }

    private sealed class NSwagOptions(NSwagCategorySnapshot snapshot) : INSwagOptions
    {
        public bool InjectHttpClient => snapshot.NSwagInjectHttpClient.ValueOrDefault(defaultValue: true);
        public bool GenerateClientInterfaces => snapshot.NSwagGenerateClientInterfaces.ValueOrDefault(defaultValue: true);
        public bool GenerateDtoTypes => snapshot.NSwagGenerateDtoTypes.ValueOrDefault(defaultValue: true);
        public CSharpClassStyle ClassStyle => ParseEnum(snapshot.NSwagClassStyle, CSharpClassStyle.Poco);
        public bool UseDocumentTitle => snapshot.NSwagUseDocumentTitle.ValueOrDefault(defaultValue: true);
        public string ParameterDateTimeFormat => snapshot.NSwagParameterDateTimeFormat.ValueOrDefault(defaultValue: "s") ?? "s";
        public bool UseBaseUrl => snapshot.NSwagUseBaseUrl.ValueOrDefault(defaultValue: false);
    }

    private sealed class NSwagStudioOptions(NSwagStudioCategorySnapshot snapshot) : INSwagStudioOptions
    {
        public bool InjectHttpClient => snapshot.NSwagStudioInjectHttpClient.ValueOrDefault(defaultValue: true);
        public bool GenerateClientInterfaces => snapshot.NSwagStudioGenerateClientInterfaces.ValueOrDefault(defaultValue: true);
        public bool GenerateDtoTypes => snapshot.NSwagStudioGenerateDtoTypes.ValueOrDefault(defaultValue: true);
        public CSharpClassStyle ClassStyle => ParseEnum(snapshot.NSwagStudioClassStyle, CSharpClassStyle.Poco);
        public bool UseDocumentTitle => snapshot.NSwagStudioUseDocumentTitle.ValueOrDefault(defaultValue: true);
        public string ParameterDateTimeFormat => snapshot.NSwagStudioParameterDateTimeFormat.ValueOrDefault(defaultValue: "s") ?? "s";
        public bool UseBaseUrl => snapshot.NSwagStudioUseBaseUrl.ValueOrDefault(defaultValue: false);
        public bool GenerateResponseClasses => snapshot.NSwagStudioGenerateResponseClasses.ValueOrDefault(defaultValue: true);
        public bool GenerateJsonMethods => snapshot.NSwagStudioGenerateJsonMethods.ValueOrDefault(defaultValue: true);
        public bool RequiredPropertiesMustBeDefined => snapshot.NSwagStudioRequiredPropertiesMustBeDefined.ValueOrDefault(defaultValue: true);
        public bool GenerateDefaultValues => snapshot.NSwagStudioGenerateDefaultValues.ValueOrDefault(defaultValue: true);
        public bool GenerateDataAnnotations => snapshot.NSwagStudioGenerateDataAnnotations.ValueOrDefault(defaultValue: true);
    }

    private sealed class OpenApiOptions(OpenApiGeneratorCategorySnapshot snapshot) : IOpenApiGeneratorOptions
    {
        public bool EmitDefaultValue => snapshot.OpenApiEmitDefaultValue.ValueOrDefault(defaultValue: true);
        public bool MethodArgument => snapshot.OpenApiMethodArgument.ValueOrDefault(defaultValue: true);
        public bool GeneratePropertyChanged => snapshot.OpenApiGeneratePropertyChanged.ValueOrDefault(defaultValue: false);
        public bool UseCollection => snapshot.OpenApiUseCollection.ValueOrDefault(defaultValue: false);
        public bool UseDateTimeOffset => snapshot.OpenApiUseDateTimeOffset.ValueOrDefault(defaultValue: false);
        public OpenApiSupportedTargetFramework TargetFramework
            => ParseEnum(snapshot.OpenApiTargetFramework, OpenApiSupportedTargetFramework.NetStandard21);
        public string? CustomAdditionalProperties
            => NormalizeEmpty(snapshot.OpenApiCustomAdditionalProperties.ValueOrDefault(defaultValue: string.Empty));
        public bool SkipFormModel => snapshot.OpenApiSkipFormModel.ValueOrDefault(defaultValue: true);
        public string? TemplatesPath => NormalizeEmpty(snapshot.OpenApiTemplatesPath.ValueOrDefault(defaultValue: string.Empty));
        public bool UseConfigurationFile => snapshot.OpenApiUseConfigurationFile.ValueOrDefault(defaultValue: true);
        public bool GenerateMultipleFiles => snapshot.OpenApiGenerateMultipleFiles.ValueOrDefault(defaultValue: false);
        public OpenApiSupportedVersion Version => ParseEnum(snapshot.OpenApiVersion, OpenApiSupportedVersion.Latest);
        public string? HttpUserAgent => NormalizeEmpty(snapshot.OpenApiHttpUserAgent.ValueOrDefault(defaultValue: string.Empty));
    }

    private sealed class RefitterOptions(RefitterCategorySnapshot snapshot) : IRefitterOptions
    {
        public bool GenerateContracts => snapshot.RefitterGenerateContracts.ValueOrDefault(defaultValue: true);
        public bool GenerateXmlDocCodeComments => snapshot.RefitterGenerateXmlDocCodeComments.ValueOrDefault(defaultValue: true);
        public bool AddAutoGeneratedHeader => snapshot.RefitterAddAutoGeneratedHeader.ValueOrDefault(defaultValue: false);
        public bool ReturnIApiResponse => snapshot.RefitterReturnIApiResponse.ValueOrDefault(defaultValue: false);
        public bool GenerateInternalTypes => snapshot.RefitterGenerateInternalTypes.ValueOrDefault(defaultValue: false);
        public bool UseCancellationTokens => snapshot.RefitterUseCancellationTokens.ValueOrDefault(defaultValue: false);
        public bool GenerateHeaderParameters => snapshot.RefitterGenerateHeaderParameters.ValueOrDefault(defaultValue: true);
        public bool GenerateMultipleFiles => snapshot.RefitterGenerateMultipleFiles.ValueOrDefault(defaultValue: false);
    }

    private sealed class KiotaOptions(KiotaCategorySnapshot snapshot) : IKiotaOptions
    {
        public bool GenerateMultipleFiles => snapshot.KiotaGenerateMultipleFiles.ValueOrDefault(defaultValue: false);
        public TypeAccessModifier TypeAccessModifier => ParseEnum(snapshot.KiotaTypeAccessModifier, TypeAccessModifier.Public);
        public bool UsesBackingStore => snapshot.KiotaUsesBackingStore.ValueOrDefault(defaultValue: false);
    }

    private sealed class TelemetryOptions(AnalyticsCategorySnapshot snapshot, VisualStudioExtensibility extensibility) : ITelemetryOptions
    {
        public bool TelemetryOptOut
        {
            get => snapshot.TelemetryOptOut.ValueOrDefault(defaultValue: false);
            set => extensibility.Settings().WriteAsync(
                batch => batch.WriteSetting(SettingDefinitions.TelemetryOptOut, value),
                description: "Update telemetry preference",
                CancellationToken.None);
        }
    }

    private static T ParseEnum<T>(SettingValue<string> value, T defaultValue)
        where T : struct
    {
        var raw = value.ValueOrDefault(defaultValue.ToString()) ?? defaultValue.ToString();
        return Enum.TryParse(raw, out T parsed) ? parsed : defaultValue;
    }

    private static string? NormalizeEmpty(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value;
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
