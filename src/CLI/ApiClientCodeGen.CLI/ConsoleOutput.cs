using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Logging;

namespace Rapicgen.CLI
{
    [ExcludeFromCodeCoverage]
    public class ConsoleOutput : IConsoleOutput
    {
        public ConsoleOutput()
        {
        }

        public void WriteLine(string value)
            => Console.WriteLine(value);
    }
}