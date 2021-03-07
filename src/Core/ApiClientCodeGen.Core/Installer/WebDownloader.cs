using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    [ExcludeFromCodeCoverage]
    public class WebDownloader : IWebDownloader
    {
        public void DownloadFile(string address, string filename)
        {
            using var client = new WebClient();
            client.DownloadFile(address, filename);
        }
    }
}