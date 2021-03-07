using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    [ExcludeFromCodeCoverage]
    public class WebDownloader : IWebDownloader
    {
        public Task DownloadFile(string address, string fileName)
        {
            new WebClient().DownloadFile(address, fileName);
            return Task.CompletedTask;
        }
    }
}