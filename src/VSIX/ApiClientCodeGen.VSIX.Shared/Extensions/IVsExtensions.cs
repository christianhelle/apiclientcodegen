using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Rapicgen.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class IVsExtensions
    {
        [SuppressMessage(
            "Usage", "VSTHRD108:Assert thread affinity unconditionally",
            Justification = "ThrowIfNotOnUIThread() causes unit tests to fail")]
        public static void Progress(
            this IVsGeneratorProgress pGenerateProgress,
            uint complete)
        {
            try
            {
                ThreadHelper.JoinableTaskFactory?.Run(
                    async () =>
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        pGenerateProgress?.Progress(complete, 100);
                    });
            }
            catch
            {
                if (!TestingUtility.IsRunningFromUnitTest)
                    ThreadHelper.ThrowIfNotOnUIThread();
                pGenerateProgress?.Progress(complete, 100);
            }
        }

        public static void GeneratorError(
            this IVsGeneratorProgress pGenerateProgress,
            Exception exception)
        {
            try
            {
                ThreadHelper.JoinableTaskFactory?.Run(
                    async () =>
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        GenerateErrorInternal(pGenerateProgress, exception);
                    });
            }
            catch
            {
                GenerateErrorInternal(pGenerateProgress, exception);
            }
        }

        [SuppressMessage(
            "Usage", "VSTHRD108:Assert thread affinity unconditionally",
            Justification = "ThrowIfNotOnUIThread() causes unit tests to fail")]
        private static void GenerateErrorInternal(IVsGeneratorProgress pGenerateProgress, Exception exception)
        {
            if (!TestingUtility.IsRunningFromUnitTest)
                ThreadHelper.ThrowIfNotOnUIThread();
            pGenerateProgress?.GeneratorError(0, 0, exception.Message, 0, 0);
            Trace.WriteLine(exception);
        }
    }
}
