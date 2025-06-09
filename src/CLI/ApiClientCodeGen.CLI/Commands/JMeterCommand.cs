using System;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.CLI.Commands
{
    public class JMeterCommand : CodeGeneratorCommand, IGeneralOptions
    {
        private readonly IJMeterCodeGeneratorFactory codeGeneratorFactory;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public JMeterCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IJMeterCodeGeneratorFactory codeGeneratorFactory,
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
                SwaggerFile,
                OutputFile,
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
    }
}