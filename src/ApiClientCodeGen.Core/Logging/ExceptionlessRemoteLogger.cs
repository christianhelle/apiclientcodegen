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
            ExceptionlessClient.Default.Configuration.AddPlugin<IgnoreNonProjectReletedExceptionsPlugin>();
            ExceptionlessClient.Default.Startup("6CRkH7zip11qalrUJgxi78lVyi93rxhQkzbYZfK2");
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