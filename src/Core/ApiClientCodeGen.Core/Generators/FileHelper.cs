using System;
using System.IO;
using System.Security.Cryptography;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators
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
    }
}