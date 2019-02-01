using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio
{
    public class NSwagStudioCodeGenerator : ICodeGenerator
    {
        private readonly string nswagStudioFile;

        public NSwagStudioCodeGenerator(string nswagStudioFile)
        {
            this.nswagStudioFile = nswagStudioFile ?? throw new ArgumentNullException(nameof(nswagStudioFile));
        }

        public string GenerateCode()
        {
            var command = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Rico Suter\\NSwagStudio\\Win\\NSwag.exe");

            if (!File.Exists(command))
                throw new NotInstalledException("NSwag not installed. Please install NSwagStudio");

            ProcessHelper.StartProcess(command, $"run {nswagStudioFile}");
            return null;
        }
    }
}
