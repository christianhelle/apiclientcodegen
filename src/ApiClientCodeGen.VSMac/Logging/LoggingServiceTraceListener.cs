using System;
using System.Diagnostics;

namespace ApiClientCodeGen.VSMac.Logging
{
    public class LoggingServiceTraceListener : TraceListener
    {
        private readonly ILoggingService loggingService;

        public LoggingServiceTraceListener(ILoggingService loggingService)
        {
            this.loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        public override void Write(string message)
            => loggingService.Log(message);

        public override void WriteLine(string message)
            => loggingService.Log(message);
    }
}