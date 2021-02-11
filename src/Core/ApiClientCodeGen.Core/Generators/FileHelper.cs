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

        public static string CalculateChecksum(string filename)
        {
            using (var hashAlgorithm = MD5.Create())
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