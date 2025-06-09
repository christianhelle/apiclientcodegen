using System;
using McMaster.Extensions.CommandLineUtils;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.CLI.Commands.CSharp
{
    [Command("swagger", Description = "Swagger Codegen CLI (v3.0.34)")]
    public class SwaggerCodegenCommand : CodeGeneratorCommand
    {
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly ISwaggerCodegenFactory factory;
        private readonly IDependencyInstaller dependencyInstaller;

        public SwaggerCodegenCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            ISwaggerCodegenFactory factory,
            IDependencyInstaller dependencyInstaller) : base(console, progressReporter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator()
            => factory.Create(
                SwaggerFile,
                DefaultNamespace,
                options,
                processLauncher,
                dependencyInstaller);
    }
}