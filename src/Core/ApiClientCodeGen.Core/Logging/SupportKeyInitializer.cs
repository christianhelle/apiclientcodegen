using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public sealed class SupportKeyInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is not ISupportProperties supportProperties)
                return;
            supportProperties.Properties["support-key"] = SupportInformation.GetSupportKey();
        }
    }
}