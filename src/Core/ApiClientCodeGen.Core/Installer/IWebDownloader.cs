namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public interface IWebDownloader
    {
        void DownloadFile(string address, string filename);
    }
}