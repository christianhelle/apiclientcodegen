using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.AutoRest
{
    public class AutoRestCSharpCodeGenerator : CodeGenerator
    {
        private readonly IAutoRestOptions options;

        public AutoRestCSharpCodeGenerator(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options)
            : base(swaggerFile, defaultNamespace)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected override string GetArguments(string outputFile)
        {
            var args = "--csharp " +
                       $"--input-file=\"{SwaggerFile}\" " +
                       $"--output-file=\"{outputFile}\" " +
                       $"--namespace=\"{DefaultNamespace}\" ";

            if (options.AddCredentials)
                args += "--add-credentials ";

            args += $"--client-side-validation=\"{options.ClientSideValidation}\" ";
            args += $"--sync-methods=\"{options.SyncMethods}\" ";

            if (options.UseDateTimeOffset)
                args += "--use-datetimeoffset ";

            if (options.UseInternalConstructors)
                args += " --use-internal-constructors ";

            if (options.OverrideClientName)
            {
                var file = new FileInfo(SwaggerFile);
                var name = file.Name
                    .Replace(" ", string.Empty)
                    .Replace(file.Extension, string.Empty);

                args += $" --override-client-name=\"{name}\"";
            }

            return args;
        }

        protected override string GetCommand()
        {
            var autorestCmd = PathProvider.GetAutoRestPath();

            if (!File.Exists(autorestCmd))
                DependencyDownloader.InstallAutoRest();

            return autorestCmd;
        }
    }
}
