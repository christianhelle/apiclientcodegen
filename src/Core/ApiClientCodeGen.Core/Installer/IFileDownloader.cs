using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public interface IFileDownloader
    {
        Task<string> DownloadFile(
            string outputFolder,
            string outputFilename,
            string checksumMd5,
            string url,
            bool forceDownload = false);
    }
}