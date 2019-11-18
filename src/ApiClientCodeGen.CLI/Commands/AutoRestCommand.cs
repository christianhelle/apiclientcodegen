using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("autorest", Description = "Generate Swagger / Open API client using AutoRest")]
    public class AutoRestCommand : SwaggerCommand
    {
        private readonly ILogger<RootCommand> logger;
        private readonly IConsole console;

        public AutoRestCommand(
            ILogger<RootCommand> logger,
            IConsole console)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }
    }
}