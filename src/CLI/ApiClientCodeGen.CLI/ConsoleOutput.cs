using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Logging;
using Spectre.Console;

namespace Rapicgen.CLI
{
    [ExcludeFromCodeCoverage]
    public class ConsoleOutput : IConsoleOutput
    {
        private readonly IAnsiConsole console;

        public ConsoleOutput(IAnsiConsole console, IVerboseOptions verboseOptions)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));

            if (verboseOptions.Enabled) 
                Trace.Listeners.Add(new ConsoleOutputTraceListener(this));
        }

        public void WriteLine(string value)
            => console.WriteLine(value);

        public void WriteMarkup(string markup)
            => console.MarkupLine(markup);

        public void Write(string value)
            => console.Write(value);
    }
}