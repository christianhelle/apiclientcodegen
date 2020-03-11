using System;
using System.Diagnostics;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using MonoDevelop.Core;
using MonoDevelop.Core.Logging;

namespace ApiClientCodeGen.VSMac
{
    public class LoggingServiceTraceListener : TraceListener
    {
        public LoggingServiceTraceListener()
            => new Action(() => LoggingService.Initialize(true))
                .SafeInvoke();

        public override void Write(string message)
            => new Action(() => LoggingService.Log(LogLevel.Info, message))
                .SafeInvoke();

        public override void WriteLine(string message)
            => new Action(() => LoggingService.Log(LogLevel.Info, message))
                .SafeInvoke();
    }
}