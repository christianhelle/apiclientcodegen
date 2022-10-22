﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Rapicgen.Windows
{
    [ExcludeFromCodeCoverage]
    public class OutputWindowTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            OutputWindow.Log(message);
        }

        public override void WriteLine(string message)
        {
            OutputWindow.Log(message);
        }
    }
}