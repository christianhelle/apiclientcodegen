using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public class AppInsightsRemoteLogger : IRemoteLogger
    {
        private readonly TelemetryClient telemetryClient;

        public AppInsightsRemoteLogger()
        {
            var configuration = TelemetryConfiguration.CreateDefault();
            configuration.ConnectionString =
                "InstrumentationKey=ad35e23f-6e54-40ff-b4d1-de6d684e1a4b;IngestionEndpoint=https://westeurope-5.in.applicationinsights.azure.com/;LiveEndpoint=https://westeurope.livediagnostics.monitor.azure.com/";

            telemetryClient = new TelemetryClient(configuration);
            telemetryClient.Context.User.Id = SupportInformation.GetSupportKey();
            telemetryClient.Context.Session.Id = Guid.NewGuid().ToString();
            telemetryClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            telemetryClient.Context.Component.Version = GetType().Assembly.GetName().Version.ToString();
            telemetryClient.TelemetryConfiguration.TelemetryInitializers.Add(new SupportKeyInitializer());
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            telemetryClient.TrackEvent(featureName);
            telemetryClient.Flush();
        }

        public void TrackError(Exception exception)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            telemetryClient.TrackException(exception);
            telemetryClient.Flush();
        }

        public void TrackDependencyFailure(string dependencyName, string? data = null)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            telemetryClient.TrackDependency(
                new DependencyTelemetry
                {
                    Name = dependencyName,
                    Success = false,
                    Data = data
                });
            telemetryClient.Flush();
        }

        public void Disable()
        {
            telemetryClient.TelemetryConfiguration.DisableTelemetry = true;
        }

        private sealed class SupportKeyInitializer : ITelemetryInitializer
        {
            public void Initialize(ITelemetry telemetry)
            {
                if (telemetry is not ISupportProperties supportProperties)
                    return;
                supportProperties.Properties["support-key"] = SupportInformation.GetSupportKey();
            }
        }
    }
}