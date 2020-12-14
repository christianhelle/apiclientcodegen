using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public class RemoteLogger : IRemoteLogger
    {
        private readonly List<IRemoteLogger> loggers = new List<IRemoteLogger>();

        public RemoteLogger(params IRemoteLogger[] remoteLoggers)
        {
            if (TestingUtility.IsRunningFromUnitTest || Debugger.IsAttached)
                return;
            
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