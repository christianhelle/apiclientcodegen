using System;
using System.Collections.Generic;
using System.Linq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public class RemoteLogger : IRemoteLogger
    {
        public List<IRemoteLogger> Loggers { get; } = new();

        public List<string> DefaultTags { get; } = new();

        public RemoteLogger(params IRemoteLogger[] remoteLoggers)
        {
            Loggers.AddRange(
                new IRemoteLogger[]
                {
                    new ExceptionlessRemoteLogger(),
                    new AppInsightsRemoteLogger(),
                });

            if (remoteLoggers.Any())
            {
                Loggers.AddRange(remoteLoggers);
            }
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            foreach (var logger in Loggers)
                logger.TrackFeatureUsage(featureName, DefaultTags.Union(tags).ToArray());
        }

        public void TrackError(Exception exception)
        {
            foreach (var logger in Loggers)
                logger.TrackError(exception);
        }

        public void TrackDependencyFailure(string dependencyName)
        {
            foreach (var logger in Loggers)
                logger.TrackDependencyFailure(dependencyName);
        }

        public void Disable() 
            => Loggers.ForEach(c=>c.Disable());
    }
}