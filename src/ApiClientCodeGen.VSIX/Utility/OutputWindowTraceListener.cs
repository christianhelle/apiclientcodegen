using System.Diagnostics;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Utility
{
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