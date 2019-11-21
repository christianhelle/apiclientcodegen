using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    public abstract class CodeGeneratorCommand
    {
        private readonly IConsole console;
        private readonly IProgressReporter progressReporter;

        protected CodeGeneratorCommand(IConsole console, IProgressReporter progressReporter)
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
        public string OutputFile { get; set; }
        
        public virtual async Task<int> OnExecuteAsync()
        {
            var generator = CreateGenerator();
            var filename = OutputFile ?? Path.GetFileNameWithoutExtension(SwaggerFile) + ".cs";
            var code = await Task.Run(() => generator.GenerateCode(progressReporter));
            await File.WriteAllTextAsync(filename, code);

            console.WriteLine($"Output file name: {filename}");
            console.WriteLine($"Output file size: {new FileInfo(filename).Length}");
            return ResultCodes.Success;
        }

        public abstract ICodeGenerator CreateGenerator();
    }
}