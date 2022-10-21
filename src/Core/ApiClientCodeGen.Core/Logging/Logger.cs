using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

        public static T GetLogger<T>() where T : IRemoteLogger
        {
            return ((RemoteLogger)Instance).Loggers.OfType<T>().First();
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