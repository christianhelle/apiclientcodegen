using System;
using ApiClientCodeGen.CLI.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("autorest", Description = "Generate Swagger / Open API client using AutoRest")]
    public class AutoRestCommand : CodeGeneratorCommand
    {
        private readonly IAutoRestOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IAutoRestCodeGeneratorFactory factory;
        private readonly IOpenApiDocumentFactory documentFactory;

        public AutoRestCommand(
            IConsoleOutput console,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IProgressReporter progressReporter,
            IAutoRestCodeGeneratorFactory factory,
            IOpenApiDocumentFactory documentFactory) : base(console, progressReporter)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
        }

        public override ICodeGenerator CreateGenerator()
            => factory.Create(
                SwaggerFile,
                DefaultNamespace,
                options,
                processLauncher,
                documentFactory);
    }
}