using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Rapicgen.Core.Generators
{
    public static class FileHelper
    {
        public static string ReadThenDelete(string outputFile)
        {
            try
            {
                return File.ReadAllText(outputFile);
            }
            finally
            {
                File.Delete(outputFile);
            }
        }

        public static string CreateRandomFile()
        {
            var outputFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.tmp");
            File.Create(outputFile).Dispose();
            return outputFile;
        }

        public static string CalculateChecksum(string filename)
        {
            using (var hashAlgorithm = SHA1.Create())
            using (var stream = File.OpenRead(filename))
            {
                return BitConverter
                    .ToString(hashAlgorithm.ComputeHash(stream))
                    .Replace("-", "")
                    .ToUpperInvariant();
            }
        }

        /// <summary>
        /// Safely reads all lines from a file, handling potential path-too-long issues on Windows
        /// </summary>
        /// <param name="path">The file to read</param>
        /// <returns>The lines from the file</returns>
        public static string[] SafeReadAllLines(string path)
        {
            try
            {
                return File.ReadAllLines(path);
            }
            catch (PathTooLongException)
            {
                // For long paths, use manual file reading approach
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan))
                using (var reader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    var lines = new System.Collections.Generic.List<string>();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                    return lines.ToArray();
                }
            }
            catch (DirectoryNotFoundException)
            {
                // If we encounter directory not found, try with a more robust approach
                // This catches cases on Windows where the directory seems correct but is too long
                try
                {
                    // Try another approach for reading the file
                    using (var fileStream = new FileStream(
                        path, 
                        FileMode.Open, 
                        FileAccess.Read, 
                        FileShare.Read, 
                        4096, 
                        FileOptions.SequentialScan | FileOptions.OpenNoBuffering))
                    using (var reader = new StreamReader(fileStream, Encoding.UTF8, true))
                    {
                        return reader.ReadToEnd().Replace("\r\n", "\n").Split('\n');
                    }
                }
                catch
                {
                    // If all approaches fail, re-throw the original exception
                    throw;
                }
            }
        }
    }
}