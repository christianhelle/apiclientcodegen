using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public interface IFileDownloader
    {
        string DownloadFile(
            string outputFilename,
            string expectedChecksumMd5,
            string url,
            bool forceDownload = false);
    }
}