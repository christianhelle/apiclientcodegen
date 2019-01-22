using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    public static class ClassNameExtractor
    {
        public static string GetClassName(string wszInputFilePath)
        {
            if (!File.Exists(wszInputFilePath))
                throw new FileNotFoundException();

            var fileInfo = new FileInfo(wszInputFilePath);
            return fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
        }
    }
}
