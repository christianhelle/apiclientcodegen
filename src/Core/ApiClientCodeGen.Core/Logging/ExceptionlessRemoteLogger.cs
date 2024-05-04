using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Exceptions;
using Exceptionless;
using Exceptionless.Plugins;

namespace Rapicgen.Core.Logging
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
            ExceptionlessClient.Default.Configuration.AddPlugin<IgnoreNonProjectReletedExceptionsPlugin>();
            ExceptionlessClient.Default.Startup("6CRkH7zip11qalrUJgxi78lVyi93rxhQkzbYZfK2");
        }

        private static bool EnableLogging()
        {
#if DEBUG
            return false;
#endif
#pragma warning disable CS0162
            return !TestingUtility.IsRunningFromUnitTest && !Debugger.IsAttached;
#pragma warning restore CS0162
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
                Trace.TraceError(e.ToString());
                TrackError(e);
            }
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;

            if (!ExceptionlessClient.Default.Configuration.Enabled)
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

            if (!ExceptionlessClient.Default.Configuration.Enabled)
                return;

            exception.ToExceptionless().Submit();
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
            ExceptionlessClient.Default.Configuration.Enabled = false;
        }

        public void Enable()
        {
            ExceptionlessClient.Default.Configuration.Enabled = true;
        }

        public void WriteLine(object data)
        {
            // Method intentionally left empty.
        }

        [Priority(30)]
        private sealed class IgnoreNonProjectReletedExceptionsPlugin : IEventPlugin
        {
            public void Run(EventPluginContext context)
            {
                if (!context.ContextData.IsUnhandledError ||
                    !context.Event.IsError() ||
                    !context.ContextData.HasException())
                {
                    context.Cancel = true;
                    return;
                }

                var exception = context.ContextData.GetException();
                context.Cancel = exception?.GetType()?.IsAssignableFrom(typeof(RapicgenException)) != true;
            }
        }
    }
}