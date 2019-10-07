using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.AutoRest
{
    public class AutoRestCSharpCodeGenerator : CodeGenerator
    {
        public AutoRestCSharpCodeGenerator(string swaggerFile, string defaultNamespace)
            : base(swaggerFile, defaultNamespace)
        {
        }

        // TODO: 
        // 1. Implement options page for AutoRest
        // 2. Inject AutoRest options to this class
        // 3. Add support for generating code using the settings exposed in the options page

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
