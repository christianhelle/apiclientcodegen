using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Rapicgen.Core.Logging
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
            Trace.WriteLine($"Feature: {featureName}");
        }

        public void TrackError(Exception exception)
        {
            foreach (var logger in Loggers)
                logger.TrackError(exception);
            Trace.WriteLine($"Error: {exception.Message}");
        }

        public void TrackDependency(
            string dependencyName,
            string? data = null,
            DateTimeOffset startTime = default,
            TimeSpan duration = default,
            bool success = false)
        {
            foreach (var logger in Loggers)
                logger.TrackDependency(dependencyName, data, startTime, duration, success);
            Trace.WriteLine($"Dependency: {dependencyName}");
        }

        public void Disable()
            => Loggers.ForEach(c => c.Disable());

        public void Enable()
            => Loggers.ForEach(c => c.Enable());

        public void WriteLine(object data)
        {
            foreach (var logger in Loggers)
                logger.WriteLine(data);
            Trace.WriteLine(data);
        }
    }
}