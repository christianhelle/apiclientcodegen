using System.Diagnostics;
using ApiClientCodeGen.VSMac.Logging;
using Rapicgen.Core.Logging;

namespace ApiClientCodeGen.VSMac
{
    public static class Bootstrapper
    {
        private static readonly object SyncLock = new();
        private static volatile bool isRegistered;
        
        public static void Initialize()
        {
            lock (SyncLock)
            {
                if (isRegistered)
                    return;
                var listener = new LoggingServiceTraceListener(new MonoDevelopLoggingService());
                Trace.Listeners.Add(listener);
                Logger.Setup().WithDefaultTags("VSMac");
                isRegistered = true;
            }
        }
    }
}
