using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.CLI.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.NSwag;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class NSwagCommandSettings : CodeGeneratorCommand<NSwagCommandSettings>.Settings, INSwagOptions
    {
        [CommandOption("--inject-http-client")]
        [Description("Set this to TRUE to generate the constructor that accepts HttpClient (default: TRUE)")]
        [DefaultValue(true)]
        public bool InjectHttpClient { get; set; } = true;

        [CommandOption("--generate-interfaces")]
        [Description("Set this to TRUE to generate client interfaces (default: TRUE)")]
        [DefaultValue(true)]
        public bool GenerateClientInterfaces { get; set; } = true;

        [CommandOption("--generate-dto-types")]
        [Description("Set this to TRUE to generate DTO types (default: TRUE)")]
        [DefaultValue(true)]
        public bool GenerateDtoTypes { get; set; } = true;

        [CommandOption("--use-base-url")]
        [Description("Set this to TRUE to include a base URL for every HTTP request (default: FALSE)")]
        [DefaultValue(false)]
        public bool UseBaseUrl { get; set; } = false;

        [CommandOption("--class-style")]
        [Description(
            @"C# Class Style. 
POCO (Plain Old C# Objects), 
Inpc (Implements INotifyPropertyChanged), 
Prism (Prism base class), 
Records (readonly POCO)")]
        public CSharpClassStyle ClassStyle { get; set; }

        [CommandOption("--document-title-as-class-name")]
        [Description(
            @"Set this to TRUE to use the OpenAPI Document Info Title as the generated class name. 
Set this to FALSE to use the filename (default: TRUE)")]
        [DefaultValue(true)]
        public bool UseDocumentTitle { get; set; } = true;

        [CommandOption("--parameter-date-time-format")]
        [Description("Specifies the format for DateTime type method parameters")]
        [DefaultValue("s")]
        public string ParameterDateTimeFormat { get; set; } = "s";
    }

    public class NSwagCommand : CodeGeneratorCommand<NSwagCommandSettings>
    {
        private readonly INSwagCodeGeneratorFactory codeGeneratorFactory;

        public NSwagCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            INSwagCodeGeneratorFactory codeGeneratorFactory)
            : base(console, progressReporter)
        {
            this.codeGeneratorFactory =
                codeGeneratorFactory ?? throw new ArgumentNullException(nameof(codeGeneratorFactory));
        }

        public override ICodeGenerator CreateGenerator(NSwagCommandSettings settings)
            => codeGeneratorFactory.Create(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                settings);
    }
}