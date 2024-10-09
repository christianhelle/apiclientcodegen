using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Rapicgen.Core.Logging
{
    public sealed class SupportInformationInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is not ISupportProperties supportProperties)
            {
                return;
            }

            supportProperties.Properties["support-key"] = SupportInformation.GetSupportKey();
            supportProperties.Properties["version"] = GetType()
                .Assembly.GetName()
                .Version.ToString();
        }
    }
}

