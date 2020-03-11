using System.Diagnostics;
using MonoDevelop.Core;
using MonoDevelop.Core.Logging;

namespace ApiClientCodeGen.VSMac
{
    public class LoggingServiceTraceListener : TraceListener
    {
        public LoggingServiceTraceListener()
        {
            LoggingService.Initialize(true);
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