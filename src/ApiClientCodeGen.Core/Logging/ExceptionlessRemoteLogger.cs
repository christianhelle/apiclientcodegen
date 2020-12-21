using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Exceptionless;
using Exceptionless.Plugins;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public class ExceptionlessRemoteLogger : IRemoteLogger
    {
        public ExceptionlessRemoteLogger()
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;

            EnableAnonymousUserTracking();
            ExceptionlessClient.Default.Configuration.AddPlugin<IgnoreNonProjectReletedExceptionsPlugin>();
            ExceptionlessClient.Default.Startup("6CRkH7zip11qalrUJgxi78lVyi93rxhQkzbYZfK2");
        }

        private void EnableAnonymousUserTracking()
        {
            try
            {
                ExceptionlessClient.Default.Configuration.SetUserIdentity(
                    SupportInformation.GetSupportKey(),
                    SupportInformation.GetAnonymousName());

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
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            exception.ToExceptionless().Submit();
        }

        [Priority(30)]
        private class IgnoreNonProjectReletedExceptionsPlugin : IEventPlugin
        {
            private static readonly List<string> HandledNamespaces = new List<string>
            {
                "ChristianHelle",
                "ApiClientCodeGen"
            };

            public void Run(EventPluginContext ctx)
            {
                if (!ctx.ContextData.IsUnhandledError || !ctx.Event.IsError() || !ctx.ContextData.HasException())
                    return;

                var exception = ctx.ContextData.GetException();
                if (exception == null)
                    return;

                var error = ctx.Event.GetError();
                if (error == null)
                    return;

                ctx.Cancel = !error.StackTrace
                    .Select(s => s.DeclaringNamespace)
                    .Distinct()
                    .Any(ns => HandledNamespaces.Any(ns.Contains));
            }
        }
    }
}