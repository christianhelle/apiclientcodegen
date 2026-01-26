using System;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core;
using Rapicgen.Core.Converters;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.AutoRest;

namespace Rapicgen.CLI.Commands.VisualBasic
{
    public class AutoRestVbCommandSettings : VisualBasicCodeGeneratorCommand<AutoRestVbCommandSettings>.Settings
    {
    }

    public class AutoRestVbCommand : VisualBasicCodeGeneratorCommand<AutoRestVbCommandSettings>
    {
        private readonly IAutoRestOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IAutoRestCodeGeneratorFactory factory;
        private readonly IOpenApiDocumentFactory documentFactory;
        private readonly IDependencyInstaller dependencyInstaller;

        public AutoRestVbCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            ILanguageConverter converter,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IAutoRestCodeGeneratorFactory factory,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller) 
            : base(console, progressReporter, converter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.dependencyInstaller =
                dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator(AutoRestVbCommandSettings settings)
            => factory.Create(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                options,
                processLauncher,
                documentFactory,
                dependencyInstaller);
    }
}
