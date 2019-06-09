using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;

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
            var autorestCmd = PathProvider.GetAutoRestPath();

            if (!File.Exists(autorestCmd)) 
                DependencyDownloader.InstallAutoRest();

            return autorestCmd;
        }
    }
}
