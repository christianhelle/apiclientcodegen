using System;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.CLI.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.CLI.Commands.CSharp
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class SwaggerCodegenCommandSettings : CodeGeneratorCommand<SwaggerCodegenCommandSettings>.Settings
    {
    }

    public class SwaggerCodegenCommand : CodeGeneratorCommand<SwaggerCodegenCommandSettings>
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
            this.dependencyInstaller =
                dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override ICodeGenerator CreateGenerator(SwaggerCodegenCommandSettings settings)
            => factory.Create(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                options,
                processLauncher,
                dependencyInstaller);
    }
}