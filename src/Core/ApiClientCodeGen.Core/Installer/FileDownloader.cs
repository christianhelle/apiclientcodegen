using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;

namespace Rapicgen.Core.Installer
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
            string expectedChecksumSha1,
            string url,
            bool forceDownload = false)
        {
            var filePath = Path.Combine(Path.GetTempPath(), outputFilename);
            if (File.Exists(filePath) &&
                string.Equals(
                    FileHelper.CalculateChecksum(filePath),
                    expectedChecksumSha1,
                    StringComparison.OrdinalIgnoreCase) &&
                !forceDownload)
                return filePath;

            Logger.Instance.WriteLine($"{outputFilename} not found. Attempting to download {outputFilename}");

            var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jar");
            downloader.DownloadFile(url, tempFile);

            Logger.Instance.WriteLine($"{outputFilename} downloaded successfully");

            if (!MoveFile(filePath, tempFile))
            {
                // If move failed, try to use the temp file directly
                if (File.Exists(tempFile))
                    return tempFile;
            }

            return filePath;
        }

        [ExcludeFromCodeCoverage]
        private static bool MoveFile(string filePath, string tempFile)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.Move(tempFile, filePath);
                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                return false;
            }
        }
    }
}