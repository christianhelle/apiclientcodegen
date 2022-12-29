using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Rapicgen.Core.Logging;

#pragma warning disable VSTHRD010 // Invoke single-threaded types on Main thread

namespace Rapicgen.Windows
{
    [ExcludeFromCodeCoverage]
    public static class OutputWindow
    {
        private static DateTimeOffset? lastError;

        public static void Log(object message)
        {
            if (lastError.HasValue && DateTimeOffset.Now - lastError.Value < TimeSpan.FromSeconds(5))
                return;

            try
            {
                var guid = new Guid("C7783FF4-55A9-422F-A3DD-4EA81E5CB6BB");
                var output = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
                output.CreatePane(ref guid, VsPackage.VsixName, 1, 1);

                IVsOutputWindowPane pane = null;
                output?.GetPane(ref guid, out pane);
                pane?.OutputStringThreadSafe($"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch (Exception e)
            {
                lastError = DateTimeOffset.Now;
                Logger.Instance.TrackError(e);
                // ignored
            }
        }
    }
}
#pragma warning restore VSTHRD010 // Invoke single-threaded types on Main thread