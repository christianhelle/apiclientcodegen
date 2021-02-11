using System.Diagnostics;
using ApiClientCodeGen.VSMac.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;

namespace ApiClientCodeGen.VSMac
{
    public static class Bootstrapper
    {
        private static readonly object syncLock = new object();
        private static volatile bool isRegistered = false;
        
        public static void Initialize()
        {
            lock (syncLock)
            {
                if (isRegistered)
                    return;
                var listener = Container.Instance.Resolve<LoggingServiceTraceListener>();
                Trace.Listeners.Add(listener);
                Logger.Setup(new SentryRemoteLogger()).WithDefaultTags("VSMac");
                isRegistered = true;
            }
        }
    }
}
