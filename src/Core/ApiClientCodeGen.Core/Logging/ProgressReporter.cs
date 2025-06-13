using System;

namespace Rapicgen.Core.Logging
{
    public class ProgressReporter(IConsoleOutput console) : IProgressReporter
    {
        private readonly IConsoleOutput console = console ?? throw new ArgumentNullException(nameof(console));

        public void Progress(uint progress, uint total = 100)
            => console.WriteLine(
                total == 100
                    ? $"{Environment.NewLine}PROGRESS: {progress}%"
                    : $"{Environment.NewLine}PROGRESS: {progress} / {total}");
    }
}