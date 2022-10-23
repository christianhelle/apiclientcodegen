using System;

namespace Rapicgen.Core.Logging
{
    public class ProgressReporter : IProgressReporter
    {
        private readonly IConsoleOutput console;
        private readonly IVerboseOptions verboseOptions;

        public ProgressReporter(IConsoleOutput console, IVerboseOptions verboseOptions)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.verboseOptions = verboseOptions ?? throw new ArgumentNullException(nameof(verboseOptions));
        }

        public void Progress(uint progress, uint total = 100)
            => console.WriteLine(
                total == 100
                    ? $"{OptionalLineBreak}PROGRESS: {progress}%"
                    : $"{OptionalLineBreak}PROGRESS: {progress} / {total}");

        private string OptionalLineBreak
            => (verboseOptions.Enabled ?Environment.NewLine : string.Empty);
    }
}