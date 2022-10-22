using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;
using Rapicgen.Core.Logging;
using Sentry;

namespace Rapicgen.CLI
{
    [ExcludeFromCodeCoverage]
    public class SentryRemoteLogger : IRemoteLogger
    {
        public SentryRemoteLogger()
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            SentrySdk.Init("https://c925c0d0b9ea438a865377e066eeb8ff@o418075.ingest.sentry.io/5555548");
            SentrySdk.ConfigureScope(ConfigureScope);
        }

        private static void ConfigureScope(Scope scope)
        {
            scope.SetTag("Platform", "CLI");
            scope.SetTag("SupportKey", SupportInformation.GetSupportKey());
            scope.SetExtra("SupportKey", SupportInformation.GetSupportKey());
            scope.User = new User
            {
                Id = SupportInformation.GetAnonymousIdentity(),
                Username = SupportInformation.GetSupportKey(),
            };
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            SentrySdk.CaptureMessage($"[CLI] {featureName}", SentryLevel.Debug);
        }

        public void TrackError(Exception exception)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            SentrySdk.CaptureException(exception);
        }

        public void TrackDependency(
            string dependencyName,
            string? data = null,
            DateTimeOffset startTime = default,
            TimeSpan duration = default,
            bool success = false)
        {
            // Not implemented
        }

        public void Disable()
        {
            SentrySdk.Close();
        }
    }
}