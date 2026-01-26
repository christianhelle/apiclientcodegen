using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core;
using Rapicgen.Core.Converters;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Kiota;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.VisualBasic
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class KiotaVbCommandSettings : VisualBasicCodeGeneratorCommand<KiotaVbCommandSettings>.Settings, IKiotaOptions
    {
        [CommandOption("--generate-multiple-files|-m")]
        [Description("Set this to TRUE to generate multiple files (default: FALSE)")]
        public bool GenerateMultipleFiles { get; set; }

        [CommandOption("--type-access-modifier")]
        [Description("Set the access modifier for the generated types (default: public)")]
        public TypeAccessModifier TypeAccessModifier { get; set; }

        [CommandOption("--backing-store|-b")]
        [Description("Generate EF backing store code in models (default: FALSE)")]
        public bool UsesBackingStore { get; set; }
    }

    public class KiotaVbCommand : VisualBasicCodeGeneratorCommand<KiotaVbCommandSettings>
    {
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public KiotaVbCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            ILanguageConverter converter,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            : base(console, progressReporter, converter)
        {
            this.processLauncher = processLauncher;
            this.dependencyInstaller = dependencyInstaller;
        }

        public override ICodeGenerator CreateGenerator(KiotaVbCommandSettings settings) =>
            new KiotaCodeGenerator(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                processLauncher,
                dependencyInstaller,
                settings);
    }
}
