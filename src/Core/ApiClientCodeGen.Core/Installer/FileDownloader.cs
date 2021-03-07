using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public class FileDownloader : IFileDownloader
    {
        private readonly IWebDownloader downloader;

        public FileDownloader(IWebDownloader downloader)
        {
            this.downloader = downloader ?? throw new ArgumentNullException(nameof(downloader));
        }

        public string DownloadFile(
            string outputFolder,
            string outputFilename,
            string checksumMd5,
            string url,
            bool forceDownload = false)
        {
            if (string.IsNullOrWhiteSpace(outputFolder))
                outputFolder = Path.Combine(Path.GetTempPath(), outputFilename);

            if (File.Exists(outputFolder) &&
                FileHelper.CalculateChecksum(outputFolder) == checksumMd5 &&
                !forceDownload)
                return outputFolder;

            Trace.WriteLine($"{outputFilename} not found. Attempting to download {outputFilename}");

            var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jar");
            downloader.DownloadFile(url, tempFile);

            Trace.WriteLine($"{outputFilename} downloaded successfully");

            try
            {
                if (File.Exists(outputFolder))
                    File.Delete(outputFolder);
                File.Move(tempFile, outputFolder);
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Trace.WriteLine(e);
            }

            return outputFolder;
        }
    }
}