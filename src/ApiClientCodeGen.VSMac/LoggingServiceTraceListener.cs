using System.Diagnostics;
using MonoDevelop.Core;
using MonoDevelop.Core.Logging;

namespace ApiClientCodeGen.VSMac
{
    public class LoggingServiceTraceListener : TraceListener
    {
        private static object syncLock = new object();
        private static volatile bool isRegistered = false;

        public LoggingServiceTraceListener()
        {
            LoggingService.Initialize(true);
        }

        public static void Initialize()
        {
            lock (syncLock)
            {
                if (isRegistered)
                    return;
                Trace.Listeners.Add(new LoggingServiceTraceListener());
                isRegistered = true;
            }
        }

        public override void Write(string message)
        {
            LoggingService.Log(LogLevel.Info, message);
        }

        public override void WriteLine(string message)
        {
            LoggingService.Log(LogLevel.Info, message);
        }
    }
}