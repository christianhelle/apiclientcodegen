using System;
using System.Diagnostics.CodeAnalysis;
using MonoDevelop.Core;

namespace ApiClientCodeGen.VSMac.Logging
{
    [ExcludeFromCodeCoverage]
    public class ProgressMonitorLoggingService : ILoggingService
    {
        private ProgressMonitor monitor;

        public ProgressMonitorLoggingService(ProgressMonitor monitor, string initialMessage)
        {
            this.monitor = monitor ?? throw new ArgumentNullException(nameof(monitor));
            monitor.BeginTask(initialMessage, 1);
        }

        public void Log(string message) => monitor.Log.WriteLine(message);

        public void Dispose()
        {
            monitor?.EndTask();
            monitor?.ReportSuccess("Done.");
            monitor?.Dispose();
            monitor = null;
        }
    }
}