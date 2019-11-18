using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("swagger", Description = "Generate Swagger / Open API client using Swagger Codegen CLI")]
    public class SwaggerCodegenCommand : SwaggerCommand
    {
        private readonly ILogger<RootCommand> logger;
        private readonly IConsole console;

        public SwaggerCodegenCommand(
            ILogger<RootCommand> logger,
            IConsole console)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }
    }
}