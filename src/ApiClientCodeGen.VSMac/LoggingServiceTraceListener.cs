using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using Microsoft.VisualStudio.Text.Editor;
using MonoDevelop.Core;
using MonoDevelop.Core.Logging;

namespace ApiClientCodeGen.VSMac
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

    public interface ILoggingService
    {
        void Log(string message);
    }

    public class DefaultLoggingService : ILoggingService
    {
        public void Log(string message)
        {
        }
    }

    [ExcludeFromCodeCoverage]
    public class MonoDevelopLoggingService : ILoggingService
    {
        public MonoDevelopLoggingService()
        {
            LoggingService.Initialize(true);
        }

        public void Log(string message)
        {
            LoggingService.Log(LogLevel.Info, message);
        }
    }
}