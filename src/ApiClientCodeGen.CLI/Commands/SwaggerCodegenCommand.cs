using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("swagger", Description = "Generate Swagger / Open API client using Swagger Codegen CLI")]
    public class SwaggerCodegenCommand : CodeGeneratorCommand
    {
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;

        public SwaggerCodegenCommand(
            IConsole console,
            IProgressReporter progressReporter,
            IGeneralOptions options,
            IProcessLauncher processLauncher) : base(console, progressReporter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
        }

        public override ICodeGenerator CreateGenerator()
            => new SwaggerCSharpCodeGenerator(
                SwaggerFile,
                DefaultNamespace,
                options,
                processLauncher);
    }
}