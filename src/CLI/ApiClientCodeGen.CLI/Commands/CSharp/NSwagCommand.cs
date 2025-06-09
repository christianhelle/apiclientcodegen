using System;
using NJsonSchema.CodeGeneration.CSharp;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.NSwag;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class NSwagCommand : CodeGeneratorCommand, INSwagOptions
    {
        private readonly IOpenApiDocumentFactory openApiDocumentFactory;
        private readonly INSwagCodeGeneratorFactory codeGeneratorFactory;

        public NSwagCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IOpenApiDocumentFactory openApiDocumentFactory,
            INSwagCodeGeneratorFactory codeGeneratorFactory)
            : base(console, progressReporter)
        {
            this.openApiDocumentFactory = openApiDocumentFactory ??
                                          throw new ArgumentNullException(nameof(openApiDocumentFactory));
            this.codeGeneratorFactory =
                codeGeneratorFactory ?? throw new ArgumentNullException(nameof(codeGeneratorFactory));
        }

        public override ICodeGenerator CreateGenerator()
            => codeGeneratorFactory.Create(
                SwaggerFile,
                DefaultNamespace,
                this,
                openApiDocumentFactory);

        // INSwagOptions implementation
        public bool InjectHttpClient { get; set; } = true;
        public bool GenerateClientInterfaces { get; set; } = true;
        public bool GenerateDtoTypes { get; set; } = true;
        public bool UseBaseUrl { get; set; } = false;
        public CSharpClassStyle ClassStyle { get; set; }
        public bool UseDocumentTitle { get; set; } = true;
        public string ParameterDateTimeFormat { get; set; } = "s";
    }
}