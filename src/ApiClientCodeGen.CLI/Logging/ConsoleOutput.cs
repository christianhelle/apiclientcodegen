using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Logging
{
    public interface IConsoleOutput
    {
        void WriteLine(string value);

    }

    [ExcludeFromCodeCoverage]
    public class ConsoleOutput : IConsoleOutput
    {
        private readonly IConsole console;

        public ConsoleOutput(IConsole console )
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console)); 
            Trace.Listeners.Add(new ConsoleOutputTraceListener(this));
        }

        public void WriteLine(string value)
            => console.Out.WriteLine(value);
    }
}