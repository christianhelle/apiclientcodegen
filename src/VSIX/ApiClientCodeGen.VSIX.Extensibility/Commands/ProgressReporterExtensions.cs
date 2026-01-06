using Rapicgen.Core.Logging;
using Microsoft.VisualStudio.RpcContracts.ProgressReporting;
using ProgressReporter = Microsoft.VisualStudio.Extensibility.Shell.ProgressReporter;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

public static class ProgressReporterExtensions
{
    public static void Report(
        this ProgressReporter reporter,
        int progressPercentage,
        string message)
    {
        Logger.Instance.WriteLine(message);
        reporter.Report(new ProgressStatus(progressPercentage, message));
    }
}
