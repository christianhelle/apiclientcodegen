using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    [ExcludeFromCodeCoverage]
    public class WebDownloader : IWebDownloader
    {
        public void DownloadFile(string address, string filename)
        {
            try
            {
                using var mutex = new Mutex(false, nameof(WebDownloader));
                if (!mutex.WaitOne(TimeSpan.FromMinutes(2)))
                    throw new TimeoutException();

                using var client = new WebClient();
                client.DownloadFile(address, filename);
            
                mutex.ReleaseMutex();
            }
            catch
            {
                Logger.Instance.TrackDependencyFailure($"GET {address}");
            }
        }
    }
}