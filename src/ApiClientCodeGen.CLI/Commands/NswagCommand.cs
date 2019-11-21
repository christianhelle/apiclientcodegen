using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("nswag", Description = "Generate Swagger / Open API client using NSwag")]
    public class NswagCommand : CodeGeneratorCommand
    {
        private readonly IOpenApiDocumentFactory openApiDocumentFactory;
        private readonly INSwagOptions options;

        public NswagCommand(
            IConsole console,
            IProgressReporter progressReporter,
            IOpenApiDocumentFactory openApiDocumentFactory,
            INSwagOptions options) 
            : base(console, progressReporter)
        {
            this.openApiDocumentFactory = openApiDocumentFactory ?? throw new ArgumentNullException(nameof(openApiDocumentFactory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override ICodeGenerator CreateGenerator() 
            => new NSwagCSharpCodeGenerator(
                SwaggerFile,
                openApiDocumentFactory,
                new NSwagCodeGeneratorSettingsFactory(DefaultNamespace, options));
    }
}