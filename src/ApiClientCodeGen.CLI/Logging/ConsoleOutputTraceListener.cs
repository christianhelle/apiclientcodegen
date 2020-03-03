using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ApiClientCodeGen.CLI.Logging
{
    [ExcludeFromCodeCoverage]
    public class ConsoleOutputTraceListener : TraceListener
    {
        public IConsoleOutput Console { get; }

        public ConsoleOutputTraceListener(IConsoleOutput console)
        {
            this.Console = console;
        }
        
        public override void Write(string message)
            => Console.WriteLine(message);

        public override void WriteLine(string message) 
            => Console.WriteLine(message);
    }
}