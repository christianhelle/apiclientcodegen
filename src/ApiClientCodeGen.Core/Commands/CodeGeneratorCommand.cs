using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using ApiClientCodeGen.CLI.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    public abstract class CodeGeneratorCommand
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter progressReporter;
        private string outputFile;

        protected CodeGeneratorCommand(IConsoleOutput console, IProgressReporter progressReporter)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
        }

        [Required]
        [FileExists]
        [Argument(0, "swaggerFile", "Path to the Swagger / Open API specification file")]
        public string SwaggerFile { get; set; }

        [Argument(1, "namespace", "Default namespace to in the generated code")]
        public string DefaultNamespace { get; set; } = "GeneratedCode";

        [Argument(2, "outputFile", "Output filename to write the generated code to. Default is the swaggerFile .cs")]
        public string OutputFile
        {
            get => outputFile ?? Path.GetFileNameWithoutExtension(SwaggerFile) + ".cs";
            set => outputFile = value;
        }

        public virtual async Task<int> OnExecuteAsync()
        {
            var generator = CreateGenerator();
            var code = await Task.Run(() => generator.GenerateCode(progressReporter));
            File.WriteAllText(OutputFile, code);

            console.WriteLine($"Output file name: {OutputFile}");
            console.WriteLine($"Output file size: {new FileInfo(OutputFile).Length}");
            return ResultCodes.Success;
        }

        public abstract ICodeGenerator CreateGenerator();
    }
}