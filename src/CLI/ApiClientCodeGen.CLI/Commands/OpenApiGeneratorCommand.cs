using System;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.CLI.Commands
{
    public class OpenApiGeneratorCommand : CodeGeneratorCommand, IGeneralOptions, IOpenApiGeneratorOptions
    {
        private readonly IOpenApiGeneratorFactory codeGeneratorFactory;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public OpenApiGeneratorCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IOpenApiGeneratorFactory codeGeneratorFactory,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            : base(console, progressReporter)
        {
            this.codeGeneratorFactory = codeGeneratorFactory ?? throw new ArgumentNullException(nameof(codeGeneratorFactory));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator()
            => codeGeneratorFactory.Create(
                "csharp",
                SwaggerFile,
                DefaultNamespace,
                this,
                processLauncher,
                dependencyInstaller);

        // IGeneralOptions implementation  
        public bool GenerateMultipleFiles { get; set; }
        public string JavaPath { get; set; } = "java";
        public string NpmPath { get; set; } = "npm";
        public string NSwagPath { get; set; } = "nswag";
        public string SwaggerCodegenPath { get; set; } = "swagger-codegen";
        public string OpenApiGeneratorPath { get; set; } = "openapi-generator";
        public bool? InstallMissingPackages { get; set; } = true;

        // IOpenApiGeneratorOptions implementation
        public bool EmitDefaultValue { get; set; } = true;
        public bool MethodArgument { get; set; } = false;
        public bool GeneratePropertyChanged { get; set; } = false;
        public bool UseCollection { get; set; } = true;
        public bool UseDateTimeOffset { get; set; } = true;
        public OpenApiSupportedTargetFramework TargetFramework { get; set; } = OpenApiSupportedTargetFramework.Net80;
        public string? CustomAdditionalProperties { get; set; } = string.Empty;
        public bool SkipFormModel { get; set; } = false;
        public string? TemplatesPath { get; set; } = string.Empty;
        public bool UseConfigurationFile { get; set; } = false;
        public OpenApiSupportedVersion Version { get; set; } = OpenApiSupportedVersion.Latest;
        public string? HttpUserAgent { get; set; } = string.Empty;
    }
}