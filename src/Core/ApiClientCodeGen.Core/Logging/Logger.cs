using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public static class Logger
    {
        private static readonly object SyncLock = new();
        private static RemoteLogger? remoteLogger;

        public static IRemoteLogger Instance
        {
            get
            {
                lock (SyncLock)
                {
                    remoteLogger ??= new RemoteLogger();
                }

                return remoteLogger;
            }
        }

        public static RemoteLogger Setup(params IRemoteLogger[] loggers)
        {
            lock (SyncLock)
            {
                remoteLogger = new RemoteLogger(loggers);
            }

            return remoteLogger;
        }

        public static void WithDefaultTags(this RemoteLogger logger, params string[] tags)
            => logger.DefaultTags.AddRange(tags);
    }
}