using System.Diagnostics;

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
                isRegistered = true;
            }
        }
    }
}