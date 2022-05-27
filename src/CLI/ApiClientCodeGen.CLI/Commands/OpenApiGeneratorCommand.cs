using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.OpenApiGenerator;
using McMaster.Extensions.CommandLineUtils;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands
{
    [Command("openapi", Description = "Generate Swagger / Open API client using OpenAPI Generator")]
    public class OpenApiGeneratorCommand : CodeGeneratorCommand, IOpenApiGeneratorOptions
    {
        private readonly IGeneralOptions options;
        private readonly IOpenApiGeneratorOptions openApiGeneratorOptions;
        private readonly IProcessLauncher processLauncher;
        private readonly IOpenApiGeneratorFactory generatorFactory;
        private readonly IDependencyInstaller dependencyInstaller;

        public OpenApiGeneratorCommand(
            IConsoleOutput console,
            IProgressReporter progressReporter,
            IGeneralOptions options,
            IOpenApiGeneratorOptions openApiGeneratorOptions,
            IProcessLauncher processLauncher,
            IOpenApiGeneratorFactory generatorFactory,
            IDependencyInstaller dependencyInstaller) : base(console, progressReporter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.openApiGeneratorOptions = openApiGeneratorOptions;
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.generatorFactory = generatorFactory ?? throw new ArgumentNullException(nameof(generatorFactory));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        [Option(
            ShortName = "emit",
            LongName = "emit-default-value",
            Description =
                "Set to true if the default value for a member should be generated in the serialization stream. " +
                "Setting the EmitDefaultValue property to false is not a recommended practice. " +
                "It should only be done if there is a specific need to do so " +
                "(such as for interoperability or to reduce data size).")]
        public bool EmitDefaultValue
        {
            get => openApiGeneratorOptions.EmitDefaultValue;
            set => openApiGeneratorOptions.EmitDefaultValue = value;
        }

        [Option(
            ShortName = "optional-args",
            LongName = "optional-method-arguments",
            Description = "C# Optional method argument, e.g. void square(int x=10) (.net 4.0+ only).")]
        public bool MethodArgument
        {
            get => openApiGeneratorOptions.MethodArgument;
            set => openApiGeneratorOptions.MethodArgument = value;
        }

        [Option(
            ShortName = "gpc",
            LongName = "generate-property-changed")]
        public bool GeneratePropertyChanged
        {
            get => openApiGeneratorOptions.GeneratePropertyChanged;
            set => openApiGeneratorOptions.GeneratePropertyChanged = value;
        }

        [Option(
            ShortName = "collection",
            LongName = "use-collection",
            Description = "Deserialize array types to Collection<T> instead of List<T>.")]
        public bool UseCollection
        {
            get => openApiGeneratorOptions.UseCollection;
            set => openApiGeneratorOptions.UseCollection = value;
        }

        [Option(
            ShortName = "datetimeoffset",
            LongName = "use-datetimeoffset",
            Description = "Use DateTimeOffset to model date-time properties")]
        public bool UseDateTimeOffset
        {
            get => openApiGeneratorOptions.UseDateTimeOffset;
            set => openApiGeneratorOptions.UseDateTimeOffset = value;
        }

        [Option(
            ShortName = "f",
            LongName = "target-framework",
            Description = "The target .NET Standard / Core / Framework version")]
        public OpenApiSupportedTargetFramework TargetFramework
        {
            get => openApiGeneratorOptions.TargetFramework;
            set => openApiGeneratorOptions.TargetFramework = value;
        }

        [Option(
            ShortName = "custom-props",
            LongName = "custom-additional-properties",
            Description = "Setting this will override all the other additional properties")]
        public string? CustomAdditionalProperties
        {
            get => openApiGeneratorOptions.CustomAdditionalProperties;
            set => openApiGeneratorOptions.CustomAdditionalProperties = value;
        }

        public override ICodeGenerator CreateGenerator()
            => generatorFactory.Create(
                SwaggerFile,
                DefaultNamespace,
                options,
                openApiGeneratorOptions,
                processLauncher,
                dependencyInstaller);
    }
}