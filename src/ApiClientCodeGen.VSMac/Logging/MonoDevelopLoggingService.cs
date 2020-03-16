using System.Diagnostics.CodeAnalysis;
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using MonoDevelop.Core.Logging;
using MonoDevelop.Ide;

namespace ApiClientCodeGen.VSMac.Logging
{
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

        public void Dispose()
        {
        }
    }
}