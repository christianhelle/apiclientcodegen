using System;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions
{
    public static class IVsExtensions
    {
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
                        pGenerateProgress.Progress(complete, 100);
                    });
            }
            catch
            {
                pGenerateProgress.Progress(complete, 100);
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
                        pGenerateProgress.GeneratorError(0, 0, exception.Message, 0, 0);
                    });
            }
            catch
            {
                pGenerateProgress.GeneratorError(0, 0, exception.Message, 0, 0);
            }
        }
    }
}
