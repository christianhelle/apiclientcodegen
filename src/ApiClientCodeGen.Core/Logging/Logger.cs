using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public static class Logger
    {
        private static readonly object SyncLock = new object();
        private static IRemoteLogger remoteLogger;

        public static IRemoteLogger Instance
        {
            get
            {
                lock (SyncLock)
                {
                    if (remoteLogger == null)
                        remoteLogger = new RemoteLogger();
                }

                return remoteLogger;
            }
        }
    }
}