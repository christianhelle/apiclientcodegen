using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Exceptionless;
using Exceptionless.Plugins;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public class ExceptionlessRemoteLogger : IRemoteLogger
    {
        public ExceptionlessRemoteLogger()
        {
            if (!EnableLogging())
                return;

            EnableAnonymousUserTracking();
            ExceptionlessClient.Default.Configuration.SetVersion(GetType().Assembly.GetName().Version);
            ExceptionlessClient.Default.Configuration.AddPlugin<IgnoreAllExceptionsPlugin>();
            ExceptionlessClient.Default.Startup("6CRkH7zip11qalrUJgxi78lVyi93rxhQkzbYZfK2");
        }

        private static bool EnableLogging()
        {
#if DEBUG
            return false;
#endif
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return false;
            return true;
        }

        private void EnableAnonymousUserTracking()
        {
            try
            {
                ExceptionlessClient.Default.Configuration.SetUserIdentity(
                    SupportInformation.GetAnonymousIdentity(),
                    SupportInformation.GetSupportKey());

                ExceptionlessClient.Default.Configuration.UseSessions();
            }
            catch (Exception e)
            {
                TraceLogger.Write(e);
                TrackError(e);
            }
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            ExceptionlessClient.Default
                .CreateFeatureUsage(featureName)
                .AddTags(tags)
                .Submit();
        }

        public void TrackError(Exception exception)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            exception.ToExceptionless().Submit();
        }

        public void Disable()
        {
            ExceptionlessClient.Default.Configuration.Enabled = false;
        }
        
        [Priority(30)]
        private class IgnoreAllExceptionsPlugin : IEventPlugin
        {
            public void Run(EventPluginContext context)
            {
                context.Cancel = true;
            }
        }
    }
}