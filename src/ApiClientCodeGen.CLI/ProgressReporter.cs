using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI
{
    public class ProgressReporter : IProgressReporter
    {
        private readonly IConsole console;

        public ProgressReporter(IConsole console)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }

        public void Progress(uint progress, uint total = 100)
            => console.Out.WriteLine(
                total == 100
                    ? $"PROGRESS: {progress}%"
                    : $"PROGRESS: {progress} / {total}");
    }
}