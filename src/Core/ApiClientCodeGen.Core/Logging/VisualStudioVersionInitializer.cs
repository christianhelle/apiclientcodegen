using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Rapicgen.Core.Logging
{
    public sealed class VisualStudioVersionInitializer : ITelemetryInitializer
    {
        private readonly Version visualStudioVersion;

        public VisualStudioVersionInitializer(Version visualStudioVersion)
        {
            this.visualStudioVersion = visualStudioVersion;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is ISupportProperties supportProperties)
                supportProperties.Properties["visual-studio-version"] = visualStudioVersion.ToString();
        }
    }
}