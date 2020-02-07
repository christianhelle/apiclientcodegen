using System;
using System.Diagnostics.CodeAnalysis;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI
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
        }

        public void WriteLine(string value)
            => console.Out.WriteLine(value);
    }
}