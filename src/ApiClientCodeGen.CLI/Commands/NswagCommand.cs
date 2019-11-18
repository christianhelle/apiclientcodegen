using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("nswag", Description = "Generate Swagger / Open API client using NSwag")]
    public class NswagCommand : SwaggerCommand
    {
        private readonly ILogger<RootCommand> logger;
        private readonly IConsole console;

        public NswagCommand(
            ILogger<RootCommand> logger,
            IConsole console)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }
    }
}