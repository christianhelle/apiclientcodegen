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
                using var context = new DependencyContext($"GET {address}");
                using var mutex = new Mutex(false, nameof(WebDownloader));
                if (!mutex.WaitOne(TimeSpan.FromMinutes(2)))
                    throw new TimeoutException();

                try
                {
                    using var client = new WebClient();
                    client.DownloadFile(address, filename);
                    context.Succeeded();
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                throw;
            }
        }
    }
}