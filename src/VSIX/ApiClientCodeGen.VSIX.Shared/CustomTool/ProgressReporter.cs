using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool
{
    public class ProgressReporter : IProgressReporter
    {
        private readonly IVsGeneratorProgress pGenerateProgress;

        public ProgressReporter(IVsGeneratorProgress pGenerateProgress)
        {
            this.pGenerateProgress = pGenerateProgress;
        }

        [SuppressMessage(
            "Usage",
            "VSTHRD010:Invoke single-threaded types on Main thread",
            Justification = "This will always throw during unit testing")]
        public void Progress(uint progress, uint total = 100)
        {
            ThrowIfNotOnUIThread();
            pGenerateProgress?.Progress(progress, total);
        }

        [ExcludeFromCodeCoverage]
        [SuppressMessage(
            "Usage",
            "VSTHRD108:Assert thread affinity unconditionally",
            Justification = "This will always throw during unit testing")]
        private static void ThrowIfNotOnUIThread()
        {
            if (!TestingUtility.IsRunningFromUnitTest)
                ThreadHelper.ThrowIfNotOnUIThread();
        }
    }
}
