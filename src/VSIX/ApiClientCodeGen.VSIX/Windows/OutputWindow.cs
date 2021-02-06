using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

#pragma warning disable VSTHRD010 // Invoke single-threaded types on Main thread

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    [ExcludeFromCodeCoverage]
    public static class OutputWindow
    {
        private static string name;
        private static IVsOutputWindowPane pane;
        private static IVsOutputWindow output;

        public static void Initialize(IServiceProvider provider, string outputSource)
        {
            if (output != null)
                return;

            ThreadHelper.ThrowIfNotOnUIThread();
            output = (IVsOutputWindow)provider.GetService(typeof(SVsOutputWindow));
            Assumes.Present(output);
            name = outputSource;

            Trace.Listeners.Add(new OutputWindowTraceListener());
        }
        
        public static void Log(object message)
        {
            try
            {
                if (EnsurePane()) 
                    pane.OutputString($"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch
            {
                // ignored
            }
        }
        
        private static bool EnsurePane()
        {
            if (pane != null)
                return true;

            var guid = Guid.NewGuid();
            output.CreatePane(ref guid, name, 1, 1);
            output.GetPane(ref guid, out pane);
            return pane != null;
        }
    }
}
#pragma warning restore VSTHRD010 // Invoke single-threaded types on Main thread