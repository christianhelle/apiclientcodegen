using System;
using System.Collections.Generic;
using System.Linq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public class RemoteLogger : IRemoteLogger
    {
        public List<IRemoteLogger> Loggers { get; } = new List<IRemoteLogger>();

        public RemoteLogger(params IRemoteLogger[] remoteLoggers)
        {
            Loggers.AddRange(
                new IRemoteLogger[]
                {
                    new ExceptionlessRemoteLogger()
                });

            if (remoteLoggers.Any())
            {
                Loggers.AddRange(remoteLoggers);
            }
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            foreach (var logger in Loggers)
                logger.TrackFeatureUsage(featureName, tags);
        }

        public void TrackError(Exception exception)
        {
            foreach (var logger in Loggers)
                logger.TrackError(exception);
        }
    }
}