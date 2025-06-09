using System;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Kiota;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class KiotaCommand : CodeGeneratorCommand, IKiotaOptions
    {
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public KiotaCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            : base(console, progressReporter)
        {
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator()
            => new KiotaCodeGenerator(
                SwaggerFile,
                DefaultNamespace,
                processLauncher,
                dependencyInstaller,
                this);

        // IKiotaOptions implementation
        public bool GenerateMultipleFiles { get; set; } = false;
        public TypeAccessModifier TypeAccessModifier { get; set; } = TypeAccessModifier.Public;
    }
}