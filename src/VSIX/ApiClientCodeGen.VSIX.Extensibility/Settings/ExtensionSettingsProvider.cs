#pragma warning disable VSEXTPREVIEW_SETTINGS
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using Microsoft.VisualStudio.Extensibility;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.AutoRest;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.Kiota;
using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.NSwagStudio;
using Rapicgen.Core.Options.OpenApiGenerator;
using Rapicgen.Core.Options.Refitter;

namespace ApiClientCodeGen.VSIX.Extensibility;

public class ExtensionSettingsProvider(VisualStudioExtensibility extensibility)
{
    public async Task<IGeneralOptions> GetGeneralOptionsAsync(CancellationToken cancellationToken)
        => new DefaultGeneralOptions();

    public async Task<IAutoRestOptions> GetAutoRestOptionsAsync(CancellationToken cancellationToken)
        => new DefaultAutoRestOptions();

    public async Task<INSwagOptions> GetNSwagOptionsAsync(CancellationToken cancellationToken)
        => new DefaultNSwagOptions();

    public async Task<INSwagStudioOptions> GetNSwagStudioOptionsAsync(CancellationToken cancellationToken)
        => new DefaultNSwagStudioOptions();

    public async Task<IOpenApiGeneratorOptions> GetOpenApiGeneratorOptionsAsync(CancellationToken cancellationToken)
        => new DefaultOpenApiGeneratorOptions();

    public async Task<IRefitterOptions> GetRefitterOptionsAsync(CancellationToken cancellationToken)
        => new DefaultRefitterOptions();

    public async Task<IKiotaOptions> GetKiotaOptionsAsync(CancellationToken cancellationToken)
        => new DefaultKiotaOptions();

    public async Task<ITelemetryOptions> GetTelemetryOptionsAsync(CancellationToken cancellationToken)
        => new TelemetryOptions(extensibility);

    private sealed class TelemetryOptions(VisualStudioExtensibility extensibility) : ITelemetryOptions
    {
        public bool TelemetryOptOut { get; set; }
    }
}
#pragma warning restore VSEXTPREVIEW_SETTINGS
