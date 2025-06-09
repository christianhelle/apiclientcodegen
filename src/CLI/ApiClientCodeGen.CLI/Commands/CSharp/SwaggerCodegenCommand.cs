using System;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class SwaggerCodegenCommand : CodeGeneratorCommand, IGeneralOptions
    {
        private readonly ISwaggerCodegenFactory codeGeneratorFactory;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public SwaggerCodegenCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            ISwaggerCodegenFactory codeGeneratorFactory,
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
                processLauncher,
                dependencyInstaller);

        // IGeneralOptions implementation  
        public bool GenerateMultipleFiles { get; set; }
    }
}