using System;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core;
using Rapicgen.Core.Converters;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.CLI.Commands.VisualBasic
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class SwaggerVbCodegenCommandSettings : VisualBasicCodeGeneratorCommand<SwaggerVbCodegenCommandSettings>.Settings
    {
    }

    public class SwaggerVbCodegenCommand : VisualBasicCodeGeneratorCommand<SwaggerVbCodegenCommandSettings>
    {
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly ISwaggerCodegenFactory factory;
        private readonly IDependencyInstaller dependencyInstaller;

        public SwaggerVbCodegenCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            ILanguageConverter converter,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            ISwaggerCodegenFactory factory,
            IDependencyInstaller dependencyInstaller) 
            : base(console, progressReporter, converter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.dependencyInstaller =
                dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator(SwaggerVbCodegenCommandSettings settings)
            => factory.Create(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                options,
                processLauncher,
                dependencyInstaller);
    }
}
