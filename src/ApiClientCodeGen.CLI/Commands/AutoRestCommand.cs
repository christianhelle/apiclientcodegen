using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("autorest", Description = "Generate Swagger / Open API client using AutoRest")]
    public class AutoRestCommand : CodeGeneratorCommand
    {
        private readonly IAutoRestOptions options;
        private readonly IProcessLauncher processLauncher;

        public AutoRestCommand(
            IConsole console,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IProgressReporter progressReporter) : base(console, progressReporter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
        }

        public override ICodeGenerator CreateGenerator()
            => new AutoRestCSharpCodeGenerator(
                SwaggerFile,
                DefaultNamespace,
                options,
                processLauncher);
    }
}