using System;
using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    [ExcludeFromCodeCoverage]
    public class OutputWindowLogger : ITraceLogger
    {
        public void Write(string message)
        {
            OutputWindow.Log(message);
        }

        public void WriteLine(string message)
        {
            OutputWindow.Log(message);
        }

        public void Write(Exception exception)
        {
            OutputWindow.Log(exception.ToString());
        }
    }
}