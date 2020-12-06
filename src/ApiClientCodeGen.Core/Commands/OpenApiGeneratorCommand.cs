using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using McMaster.Extensions.CommandLineUtils;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Commands
{
    [Command("openapi", Description = "Generate Swagger / Open API client using OpenAPI Generator")]
    public class OpenApiGeneratorCommand : CodeGeneratorCommand
    {
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IOpenApiGeneratorFactory generatorFactory;

        public OpenApiGeneratorCommand(
            IConsoleOutput console,
            IProgressReporter progressReporter,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IOpenApiGeneratorFactory generatorFactory) : base(console, progressReporter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.generatorFactory = generatorFactory ?? throw new ArgumentNullException(nameof(generatorFactory));
        }

        public override ICodeGenerator CreateGenerator()
            => generatorFactory.Create(
                SwaggerFile,
                DefaultNamespace,
                options,
                processLauncher);
    }
}