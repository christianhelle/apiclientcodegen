using System;
using McMaster.Extensions.CommandLineUtils;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.AutoRest;

namespace Rapicgen.CLI.Commands.CSharp
{
    [Command("autorest", Description = "AutoRest (v3.0.0-beta.20210504.2)")]
    public class AutoRestCommand : CodeGeneratorCommand
    {
        private readonly IAutoRestOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IAutoRestCodeGeneratorFactory factory;
        private readonly IOpenApiDocumentFactory documentFactory;
        private readonly IDependencyInstaller dependencyInstaller;

        public AutoRestCommand(
            IConsoleOutput console,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IProgressReporter? progressReporter,
            IAutoRestCodeGeneratorFactory factory,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller) : base(console, progressReporter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator()
            => factory.Create(
                SwaggerFile,
                DefaultNamespace,
                options,
                processLauncher,
                documentFactory,
                dependencyInstaller);
    }
}