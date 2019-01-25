using System.IO;
using EnvDTE;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public class FileHelper
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
    }
}