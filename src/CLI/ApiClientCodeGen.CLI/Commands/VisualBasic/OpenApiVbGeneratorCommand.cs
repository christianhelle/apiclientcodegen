using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core;
using Rapicgen.Core.Converters;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.VisualBasic
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class OpenApiVbGeneratorCommandSettings :
        VisualBasicCodeGeneratorCommand<OpenApiVbGeneratorCommandSettings>.Settings, IOpenApiGeneratorOptions
    {
        [CommandOption("--emit-default-value")]
        [Description(
            "Set to true if the default value for a member should be generated in the serialization stream.")]
        public bool EmitDefaultValue { get; set; } = true;

        [CommandOption("--method-argument")]
        [Description("C# Optional method argument, e.g. void square(int x=10) (.net 4.0+ only).")]
        public bool MethodArgument { get; set; } = true;

        [CommandOption("--generate-property-changed")]
        [Description("Generate PropertyChanged notifications")]
        public bool GeneratePropertyChanged { get; set; }

        [CommandOption("--use-collection")]
        [Description("Use collection instead of list")]
        public bool UseCollection { get; set; }

        [CommandOption("--use-datetimeoffset")]
        [Description("Use DateTimeOffset to model date-time properties")]
        public bool UseDateTimeOffset { get; set; }

        [CommandOption("--target-framework")]
        [Description("The target .NET Standard / Core / Framework version")]
        public OpenApiSupportedTargetFramework TargetFramework { get; set; } =
            OpenApiSupportedTargetFramework.NetStandard21;

        [CommandOption("--custom-additional-properties-props")]
        [Description("Setting this will override all the other additional properties")]
        public string? CustomAdditionalProperties { get; set; }

        [CommandOption("--skip-form-model")]
        [Description("To skip models defined as the form parameters in 'requestBody'")]
        public bool SkipFormModel { get; set; } = true;

        [CommandOption("--templates-path")]
        [Description("Path to directory containing additional mustache template files")]
        public string? TemplatesPath { get; set; }

        [CommandOption("--use-configuration-file")]
        [Description("Use the configuration file if present")]
        public bool UseConfigurationFile { get; set; } = true;

        [CommandOption("--generate-multiple-files")]
        [Description("Generate multiple files for each operation")]
        public bool GenerateMultipleFiles { get; set; }

        [CommandOption("--version")]
        [Description("The version of the generator to use")]
        public OpenApiSupportedVersion Version { get; set; }

        [CommandOption("--http-user-agent")]
        [Description("Sets the User-Agent header value to be sent in the HTTP request")]
        public string? HttpUserAgent { get; set; }
    }

    public class OpenApiVbGeneratorCommand : VisualBasicCodeGeneratorCommand<OpenApiVbGeneratorCommandSettings>
    {
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IOpenApiCSharpGeneratorFactory cSharpGeneratorFactory;
        private readonly IDependencyInstaller dependencyInstaller;

        public OpenApiVbGeneratorCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            ILanguageConverter converter,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IOpenApiCSharpGeneratorFactory cSharpGeneratorFactory,
            IDependencyInstaller dependencyInstaller) 
            : base(console, progressReporter, converter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.cSharpGeneratorFactory = cSharpGeneratorFactory ??
                                          throw new ArgumentNullException(nameof(cSharpGeneratorFactory));
            this.dependencyInstaller =
                dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator(OpenApiVbGeneratorCommandSettings settings)
            => cSharpGeneratorFactory.Create(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                options,
                settings,
                processLauncher,
                dependencyInstaller);
    }
}
