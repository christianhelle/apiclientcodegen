using System;
using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public class RemoteLogger : IRemoteLogger
    {
        private readonly List<IRemoteLogger> loggers = new List<IRemoteLogger>();

        public RemoteLogger()
        {
            loggers.AddRange(
                new IRemoteLogger[]
                {
                    new ExceptionlessRemoteLogger()
                });
        }
    
        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            foreach (var logger in loggers) 
                logger.TrackFeatureUsage(featureName, tags);
        }

        public void TrackEvent(string message, string source, params string[] tags)
        {
            foreach (var logger in loggers)
                logger.TrackEvent(message, source, tags);
        }

        public void TrackError(Exception exception)
        {
            foreach (var logger in loggers)
                logger.TrackError(exception);
        }
    }
}