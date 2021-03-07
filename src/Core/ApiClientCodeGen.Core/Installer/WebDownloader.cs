using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    [ExcludeFromCodeCoverage]
    public class WebDownloader : IWebDownloader
    {
        public async Task DownloadFile(string address, string fileName)
            => await new WebClient()
                .DownloadFileTaskAsync(
                    address,
                    fileName)
                .ConfigureAwait(false);
    }
}