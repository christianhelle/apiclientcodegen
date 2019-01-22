using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public class NSwagCSharpCodeGenerator : CodeGenerator
    {
        public NSwagCSharpCodeGenerator(string swaggerFile, string defaultNamespace)
            : base(swaggerFile, defaultNamespace)
        {
        }

        protected override string GetArguments(string outputFile)
            => $"swagger2csclient " +
                $"/classname:ApiClient " +
                $"/input:\"{swaggerFile}\" " +
                $"/output:\"{outputFile}\" " +
                $"/namespace:{defaultNamespace}";

        protected override string GetCommand()
        {
            var command = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Rico Suter\\NSwagStudio\\Win\\NSwag.exe");

            if (!File.Exists(command))
                throw new NotInstalledException("NSwag not installed. Please install NSwagStudio");
            
            return command;
        }
    }
}
