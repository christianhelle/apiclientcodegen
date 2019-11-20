using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command("nswagstudio", Description = "Generate Swagger / Open API client using NSwag Studio")]
    public class NswagStudioCommand : CodeGeneratorCommand
    {
        public NswagStudioCommand(
            IConsole console,
            IProgressReporter progressReporter) : base(console, progressReporter)
        {
        }

        public override ICodeGenerator CreateGenerator()
        {
            throw new NotImplementedException();
        }
    }
}