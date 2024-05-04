using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Rapicgen.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public class AppInsightsRemoteLogger : IRemoteLogger
    {
        private readonly TelemetryClient? telemetryClient;

        public AppInsightsRemoteLogger()
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;

            var configuration = TelemetryConfiguration.CreateDefault();
            configuration.ConnectionString =
                "InstrumentationKey=ad35e23f-6e54-40ff-b4d1-de6d684e1a4b;IngestionEndpoint=https://westeurope-5.in.applicationinsights.azure.com/;LiveEndpoint=https://westeurope.livediagnostics.monitor.azure.com/";

            telemetryClient = new TelemetryClient(configuration);
            telemetryClient.Context.User.Id = SupportInformation.GetSupportKey();
            telemetryClient.Context.Session.Id = Guid.NewGuid().ToString();
            telemetryClient.Context.Operation.Id = Guid.NewGuid().ToString();
            telemetryClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            telemetryClient.Context.Component.Version = GetType().Assembly.GetName().Version.ToString();
            AddTelemetryInitializer(new SupportKeyInitializer());
        }

        public void AddTelemetryInitializer(ITelemetryInitializer initializer)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached || telemetryClient == null)
                return;

            if (telemetryClient.TelemetryConfiguration.DisableTelemetry)
                return;

            telemetryClient.TelemetryConfiguration.TelemetryInitializers.Add(initializer);
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached || telemetryClient == null)
                return;

            if (telemetryClient.TelemetryConfiguration.DisableTelemetry)
                return;

            telemetryClient.TrackEvent(featureName);
            telemetryClient.Flush();
        }

        public void TrackError(Exception exception)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached || telemetryClient == null)
                return;

            if (telemetryClient.TelemetryConfiguration.DisableTelemetry)
                return;

            telemetryClient.TrackException(exception);
            telemetryClient.Flush();
        }

        public void TrackDependency(
            string dependencyName,
            string? data = null,
            DateTimeOffset startTime = default,
            TimeSpan duration = default,
            bool success = false)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached || telemetryClient == null)
                return;

            if (telemetryClient.TelemetryConfiguration.DisableTelemetry)
                return;

            telemetryClient.TrackDependency(
                new DependencyTelemetry
                {
                    Name = dependencyName,
                    Success = success,
                    Data = data,
                    Timestamp = startTime == default ? DateTimeOffset.UtcNow : startTime,
                    Duration = duration == default ? TimeSpan.Zero : duration
                });
            telemetryClient.Flush();
        }

        public void Disable()
        {
            if (telemetryClient != null)
                telemetryClient.TelemetryConfiguration.DisableTelemetry = true;
        }

        public void Enable()
        {
            if (telemetryClient != null)
                telemetryClient.TelemetryConfiguration.DisableTelemetry = false;
        }

        public void WriteLine(object data)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached || telemetryClient == null)
                return;

            if (telemetryClient.TelemetryConfiguration.DisableTelemetry)
                return;

            var message = data?.ToString();
            if (string.IsNullOrWhiteSpace(message))
                return;

            telemetryClient.TrackTrace(message);
            telemetryClient.Flush();
        }
    }
}