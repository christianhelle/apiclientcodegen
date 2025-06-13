using System;
using Spectre.Console.Cli;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.AutoRest;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class AutoRestCommand : CodeGeneratorCommand<AutoRestCommand.AutoRestSettings>
    {
        private readonly IAutoRestOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IAutoRestCodeGeneratorFactory factory;
        private readonly IOpenApiDocumentFactory documentFactory;
        private readonly IDependencyInstaller dependencyInstaller;

        public class AutoRestSettings : CodeGeneratorCommand<AutoRestCommand.AutoRestSettings>.Settings
        {
        }

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
            this.dependencyInstaller =
                dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator(AutoRestSettings settings)
            => factory.Create(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                options,
                processLauncher,
                documentFactory,
                dependencyInstaller);
    }
}