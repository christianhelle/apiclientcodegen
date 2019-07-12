using System;
using System.IO;
using System.Security.Cryptography;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
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

        public static string CalculateMd5(string filename)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filename))
                return BitConverter
                    .ToString(md5.ComputeHash(stream))
                    .Replace("-", "")
                    .ToUpperInvariant();
        }
    }
}