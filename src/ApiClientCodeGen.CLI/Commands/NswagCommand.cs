using System;
using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("nswag", Description = "Generate Swagger / Open API client using NSwag")]
    public class NswagCommand : SwaggerCommand
    {
        private readonly IConsole console;
        private readonly IProgressReporter progressReporter;
        private readonly IOpenApiDocumentFactory openApiDocumentFactory;
        private readonly INSwagOptions options;

        public NswagCommand(
            IConsole console,
            IProgressReporter progressReporter,
            IOpenApiDocumentFactory openApiDocumentFactory,
            INSwagOptions options)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
            this.openApiDocumentFactory = openApiDocumentFactory ?? throw new ArgumentNullException(nameof(openApiDocumentFactory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override async Task<int> OnExecuteAsync()
        {
            var generator = new NSwagCSharpCodeGenerator(
                SwaggerFile,
                openApiDocumentFactory,
                new NSwagCodeGeneratorSettingsFactory(DefaultNamespace, options));

            var filename = OutputFile ?? Path.GetFileNameWithoutExtension(SwaggerFile) + ".cs";
            var code = await Task.Run(() => generator.GenerateCode(progressReporter));
            await File.WriteAllTextAsync(filename, code);

            console.WriteLine($"Output file name: {filename}");
            console.WriteLine($"Output file size: {new FileInfo(filename).Length}");

            return ResultCodes.Success;
        }
    }
}