using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
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
            string outputFilename,
            string checksumMd5,
            string url,
            bool forceDownload = false)
        {
            var filePath = Path.Combine(Path.GetTempPath(), outputFilename);
            if (File.Exists(filePath) &&
                FileHelper.CalculateChecksum(filePath) == checksumMd5 &&
                !forceDownload)
                return filePath;

            TraceLogger.WriteLine($"{outputFilename} not found. Attempting to download {outputFilename}");

            var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jar");
            downloader.DownloadFile(url, tempFile);

            TraceLogger.WriteLine($"{outputFilename} downloaded successfully");

            MoveFile(filePath, tempFile);

            return filePath;
        }

        [ExcludeFromCodeCoverage]
        private static void MoveFile(string filePath, string tempFile)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.Move(tempFile, filePath);
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
            }
        }
    }
}