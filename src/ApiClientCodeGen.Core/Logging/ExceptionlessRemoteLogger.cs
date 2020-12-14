using System;
using System.Diagnostics.CodeAnalysis;
using Exceptionless;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public class ExceptionlessRemoteLogger : IRemoteLogger
    {
        public ExceptionlessRemoteLogger()
        {
            ExceptionlessClient.Default.Startup("6CRkH7zip11qalrUJgxi78lVyi93rxhQkzbYZfK2");
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            ExceptionlessClient.Default
                .CreateFeatureUsage(featureName)
                .AddTags(tags)
                .Submit();
        }

        public void TrackError(Exception exception)
        {
            exception.ToExceptionless().Submit();
        }
    }
}