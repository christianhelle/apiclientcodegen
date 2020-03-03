using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ApiClientCodeGen.CLI.Logging
{
    [ExcludeFromCodeCoverage]
    public class ConsoleOutputTraceListener : TraceListener
    {
        private readonly IConsoleOutput console;

        public ConsoleOutputTraceListener(IConsoleOutput console)
        {
            this.console = console;
        }
        
        public override void Write(string message)
            => console.WriteLine(message);

        public override void WriteLine(string message) 
            => console.WriteLine(message);
    }
}