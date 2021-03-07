using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    [ExcludeFromCodeCoverage]
    public class WebDownloader : IWebDownloader
    {
        public async Task DownloadFile(string address, string filename)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetStreamAsync(new Uri(address));
            using var fileStream = File.Create(filename);
            await response.CopyToAsync(fileStream);
        }
    }
}