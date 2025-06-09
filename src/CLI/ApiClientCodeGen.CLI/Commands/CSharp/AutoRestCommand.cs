using System;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.AutoRest;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class AutoRestCommand : CodeGeneratorCommand, IAutoRestOptions
    {
        private readonly IAutoRestCodeGeneratorFactory codeGeneratorFactory;
        private readonly IProcessLauncher processLauncher;
        private readonly IOpenApiDocumentFactory documentFactory;
        private readonly IDependencyInstaller dependencyInstaller;

        public AutoRestCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IAutoRestCodeGeneratorFactory codeGeneratorFactory,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller)
            : base(console, progressReporter)
        {
            this.codeGeneratorFactory = codeGeneratorFactory ?? throw new ArgumentNullException(nameof(codeGeneratorFactory));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator()
            => codeGeneratorFactory.Create(
                SwaggerFile,
                DefaultNamespace,
                this,
                processLauncher,
                documentFactory,
                dependencyInstaller);

        // IAutoRestOptions implementation
        public bool AddCredentials { get; set; } = false;
        public bool OverrideClientName { get; set; } = false;
        public bool UseInternalConstructors { get; set; } = false;
        public SyncMethodOptions SyncMethods { get; set; } = SyncMethodOptions.Essential;
        public bool UseDateTimeOffset { get; set; } = true;
        public bool ClientSideValidation { get; set; } = true;
    }
}