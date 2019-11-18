using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("nswagstudio", Description = "Generate Swagger / Open API client using NSwag Studio")]
    public class NswagStudioCommand : SwaggerCommand
    {
        private readonly ILogger<RootCommand> logger;
        private readonly IConsole console;

        public NswagStudioCommand(
            ILogger<RootCommand> logger,
            IConsole console)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }
    }
}