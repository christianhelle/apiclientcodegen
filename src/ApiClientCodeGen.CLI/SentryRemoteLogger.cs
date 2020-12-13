using System;
using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using Sentry;
using Sentry.Protocol;

namespace ApiClientCodeGen.CLI
{
    [ExcludeFromCodeCoverage]
    public class SentryRemoteLogger : IRemoteLogger
    {
        public SentryRemoteLogger()
        {
            SentrySdk.Init("https://c925c0d0b9ea438a865377e066eeb8ff@o418075.ingest.sentry.io/5555548");
        }
        
        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            SentrySdk.CaptureMessage(featureName, SentryLevel.Debug);
        }

        public void TrackEvent(string message, string source, params string[] tags)
        {
        }

        public void TrackError(Exception exception)
        {
            SentrySdk.CaptureException(exception);
        }
    }
}