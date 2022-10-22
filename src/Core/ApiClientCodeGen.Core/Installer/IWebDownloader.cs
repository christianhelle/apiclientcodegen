namespace Rapicgen.Core.Installer
{
    public interface IWebDownloader
    {
        void DownloadFile(string address, string filename);
    }
}