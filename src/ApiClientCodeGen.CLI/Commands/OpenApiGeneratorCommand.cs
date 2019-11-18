using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("openapi", Description = "Generate Swagger / Open API client using OpenAPI Generator")]
    public class OpenApiGeneratorCommand : SwaggerCommand
    {
        private readonly ILogger<RootCommand> logger;
        private readonly IConsole console;

        public OpenApiGeneratorCommand(
            ILogger<RootCommand> logger,
            IConsole console)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }
    }
}