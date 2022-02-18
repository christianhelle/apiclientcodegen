using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.OpenApiGenerator;
using McMaster.Extensions.CommandLineUtils;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Commands
{
    [Command("openapi", Description = "Generate Swagger / Open API client using OpenAPI Generator")]
    public class OpenApiGeneratorCommand : CodeGeneratorCommand
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