using System.Diagnostics.CodeAnalysis;
using MonoDevelop.Ide;

namespace ApiClientCodeGen.VSMac.Logging
{
    [ExcludeFromCodeCoverage]
    public class ToolOutputProgressMonitorLoggingService : ProgressMonitorLoggingService
    {
        public ToolOutputProgressMonitorLoggingService() 
            : base(
                IdeApp.Workbench.ProgressMonitors.GetToolOutputProgressMonitor(true), 
                "Starting...")
        {
        }
    }
}