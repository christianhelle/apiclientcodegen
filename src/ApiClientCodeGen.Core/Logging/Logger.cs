namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public static class Logger
    {
        private static readonly object SyncLock = new object();
        private static ILogger logger;

        public static ILogger Instance
        {
            get
            {
                lock (SyncLock)
                {
                    if (logger == null)
                        logger = new ExceptionlessLogger();
                }

                return logger;
            }
        }
    }
}