using System;
using McMaster.Extensions.CommandLineUtils;
using NJsonSchema.CodeGeneration.CSharp;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.NSwag;

namespace Rapicgen.CLI.Commands.CSharp
{
    [Command("nswag", Description = "Generate C# API client using NSwag")]
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

        [Option(
            ShortName = "ihc",
            LongName = "injectHttpClient",
            Description = "Set this to TRUE to generate the constructor that accepts HttpClient (default: TRUE)")]
        public bool InjectHttpClient { get; set; } = true;

        [Option(
            ShortName = "gi",
            LongName = "generateInterfaces",
            Description = "Set this to TRUE to generate client interfaces (default: TRUE)")]
        public bool GenerateClientInterfaces { get; set; } = true;

        [Option(
            ShortName = "dto",
            LongName = "generateDtoTypes",
            Description = "Set this to TRUE to generate DTO types (default: TRUE)")]
        public bool GenerateDtoTypes { get; set; } = true;

        [Option(
            ShortName = "baseUrl",
            LongName = "useBaseUrl",
            Description = "Set this to TRUE to include a base URL for every HTTP request (default: FALSE)")]
        public bool UseBaseUrl { get; set; } = false;

        [Option(
            ShortName = "style",
            LongName = "classStyle",
            Description =
                @"C# Class Style. 
POCO (Plain Old C# Objects), 
Inpc (Implements INotifyPropertyChanged), 
Prism (Prism base class), 
Records (readonly POCO)")]
        public CSharpClassStyle ClassStyle { get; set; }

        [Option(
            ShortName = "useTitle",
            LongName = "documentTitleAsClassName",
            Description =
                @"Set this to TRUE to use the OpenAPI Document Info Title as the generated class name. 
Set this to FALSE to use the filename (default: TRUE)")]
        public bool UseDocumentTitle { get; set; } = true;
        
        [Option(
            ShortName = "pdtf",
            LongName = "parameterDateTimeFormat",
            Description = "Specifies the format for DateTime type method parameters")]
        public string ParameterDateTimeFormat { get; set; } = "s";
    }
}