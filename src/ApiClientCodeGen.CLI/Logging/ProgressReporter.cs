using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ApiClientCodeGen.CLI
{
    public class ProgressReporter : IProgressReporter
    {
        private readonly IConsoleOutput console;

        public ProgressReporter(IConsoleOutput console)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }

        public void Progress(uint progress, uint total = 100)
            => console.WriteLine(
                total == 100
                    ? $"PROGRESS: {progress}%"
                    : $"PROGRESS: {progress} / {total}");
    }
}