using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public class AutoRestCSharpGenerator : CodeGenerator
    {
        public AutoRestCSharpGenerator(string swaggerFile, string defaultNamespace)
            : base(swaggerFile, defaultNamespace)
        {
        }

        protected override string GetArguments(string outputFile) 
            => $"--csharp " +
               $"--input-file=\"{swaggerFile}\" " +
               $"--output-file=\"{outputFile}\" " +
               $"--namespace=\"{defaultNamespace}\" " +
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
