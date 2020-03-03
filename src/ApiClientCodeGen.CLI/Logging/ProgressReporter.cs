using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ApiClientCodeGen.CLI.Logging
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
                    ? $"{OptionalLineBreak}PROGRESS: {progress}%"
                    : $"{OptionalLineBreak}PROGRESS: {progress} / {total}");

        private static string OptionalLineBreak
            => (VerboseOption.Enabled ?Environment.NewLine : string.Empty);
    }
}