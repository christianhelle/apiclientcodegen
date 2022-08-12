using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions;
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
            ExceptionlessClient.Default
                .CreateFeatureUsage(featureName)
                .AddTags(tags)
                .Submit();
        }

        public void TrackError(Exception exception)
        {
            // if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
            //     return;
            // exception.ToExceptionless().Submit();
        }

        public void Disable()
        {
            ExceptionlessClient.Default.Configuration.Enabled = false;
        }

        [Priority(30)]
        private sealed class IgnoreNonProjectReletedExceptionsPlugin : IEventPlugin
        {
            public void Run(EventPluginContext context)
            {
                context.Cancel = true;
                // if (!context.ContextData.IsUnhandledError || !context.Event.IsError() || !context.ContextData.HasException())
                //     return;
                //
                // var exception = context.ContextData.GetException();
                // context.Cancel = exception?.GetType()?.IsAssignableFrom(typeof(RapicgenException)) != true;
            }
        }
    }
}