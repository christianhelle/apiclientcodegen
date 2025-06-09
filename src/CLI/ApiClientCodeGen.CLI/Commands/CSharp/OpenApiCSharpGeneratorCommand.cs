using System;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class OpenApiCSharpGeneratorCommand : CodeGeneratorCommand, IGeneralOptions, IOpenApiGeneratorOptions
    {
        private readonly IOpenApiCSharpGeneratorFactory codeGeneratorFactory;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public OpenApiCSharpGeneratorCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IOpenApiCSharpGeneratorFactory codeGeneratorFactory,
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
                DefaultNamespace,
                this,
                this,
                processLauncher,
                dependencyInstaller);

        // IGeneralOptions implementation  
        public bool GenerateMultipleFiles { get; set; }

        // IOpenApiGeneratorOptions implementation
        public string JavaPath { get; set; } = "java";
    }
}