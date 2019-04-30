using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.AutoRest
{
    public class AutoRestCSharpCodeGenerator : CodeGenerator
    {
        public AutoRestCSharpCodeGenerator(string swaggerFile, string defaultNamespace)
            : base(swaggerFile, defaultNamespace)
        {
        }

        protected override string GetArguments(string outputFile) 
            => $"--csharp " +
               $"--input-file=\"{SwaggerFile}\" " +
               $"--output-file=\"{outputFile}\" " +
               $"--namespace=\"{DefaultNamespace}\" " +
               $"--add-credentials";

        protected override string GetCommand()
        {
            var autorestCmd = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "npm\\autorest.cmd");

            if (!File.Exists(autorestCmd))
                throw new NotInstalledException("AutoRest not installed. Please install this through NPM");

            return autorestCmd;
        }
    }
}
