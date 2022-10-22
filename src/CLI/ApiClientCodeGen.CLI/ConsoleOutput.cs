using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Logging;
using McMaster.Extensions.CommandLineUtils;

namespace Rapicgen.CLI
{
    [ExcludeFromCodeCoverage]
    public class ConsoleOutput : IConsoleOutput
    {
        private readonly IConsole console;

        public ConsoleOutput(IConsole console, IVerboseOptions verboseOptions)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));

            if (verboseOptions.Enabled) 
                Trace.Listeners.Add(new ConsoleOutputTraceListener(this));
        }

        public void WriteLine(string value)
            => console.Out.WriteLine(value);
    }
}