using System;
using System.Collections.Generic;
using System.Linq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public class RemoteLogger : IRemoteLogger
    {
        private readonly List<IRemoteLogger> loggers = new List<IRemoteLogger>();

        public RemoteLogger(params IRemoteLogger[] remoteLoggers)
        {
            loggers.AddRange(
                new IRemoteLogger[]
                {
                    new ExceptionlessRemoteLogger()
                });

            if (remoteLoggers.Any())
            {
                loggers.AddRange(remoteLoggers);
            }
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            foreach (var logger in loggers)
                logger.TrackFeatureUsage(featureName, tags);
        }

        public void TrackError(Exception exception)
        {
            foreach (var logger in loggers)
                logger.TrackError(exception);
        }
    }
}