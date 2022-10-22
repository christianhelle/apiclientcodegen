using System;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.NSwag;
using McMaster.Extensions.CommandLineUtils;

namespace Rapicgen.CLI.Commands
{
    [Command("nswag", Description = "Generate C# API client using NSwag")]
    public class NSwagCommand : CodeGeneratorCommand
    {
        private readonly IOpenApiDocumentFactory openApiDocumentFactory;
        private readonly INSwagOptions options;
        private readonly INSwagCodeGeneratorFactory codeGeneratorFactory;

        public NSwagCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
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