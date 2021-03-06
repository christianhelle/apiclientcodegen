using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public interface IWebDownloader
    {
        Task DownloadFile(string address, string fileName);
    }
}