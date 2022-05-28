using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool
{
    public class ProgressReporter : IProgressReporter
    {
        private readonly IVsGeneratorProgress pGenerateProgress;

        public ProgressReporter(IVsGeneratorProgress pGenerateProgress)
        {
            this.pGenerateProgress = pGenerateProgress;
        }

        public void Progress(uint progress, uint total = 100)
        {
            ThrowIfNotOnUIThread();
            pGenerateProgress?.Progress(progress, total);
        }

        [ExcludeFromCodeCoverage]
        private static void ThrowIfNotOnUIThread()
        {
            if (!TestingUtility.IsRunningFromUnitTest)
                ThreadHelper.ThrowIfNotOnUIThread();
        }
    }
}
