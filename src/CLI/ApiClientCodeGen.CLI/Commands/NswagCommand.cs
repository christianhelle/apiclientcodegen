using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using McMaster.Extensions.CommandLineUtils;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands
{
    [Command("nswag", Description = "Generate Swagger / Open API client using NSwag")]
    public class NSwagCommand : CodeGeneratorCommand
    {
        private readonly IOpenApiDocumentFactory openApiDocumentFactory;
        private readonly INSwagOptions options;
        private readonly INSwagCodeGeneratorFactory codeGeneratorFactory;

        public NSwagCommand(
            IConsoleOutput console,
            IProgressReporter progressReporter,
            IOpenApiDocumentFactory openApiDocumentFactory,
            INSwagOptions options,
            INSwagCodeGeneratorFactory codeGeneratorFactory) 
            : base(console, progressReporter)    
        {
            this.openApiDocumentFactory = openApiDocumentFactory ?? throw new ArgumentNullException(nameof(openApiDocumentFactory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.codeGeneratorFactory = codeGeneratorFactory ?? throw new ArgumentNullException(nameof(codeGeneratorFactory));
        }

        public override ICodeGenerator CreateGenerator()
            => codeGeneratorFactory.Create(
                SwaggerFile,
                DefaultNamespace,
                options,
                openApiDocumentFactory);
    }
}