using System;
using System.Reflection;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    [Command(Name = "run", ThrowOnUnexpectedArgument = false, OptionsComparison = StringComparison.InvariantCultureIgnoreCase)]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [Subcommand(
        typeof(AutoRestCommand),
        typeof(NswagCommand),
        typeof(NswagStudioCommand),
        typeof(SwaggerCodegenCommand),
        typeof(OpenApiGeneratorCommand))]
    public class RootCommand
    {
        [Option(VerboseOption.Template, CommandOptionType.NoValue, Description = VerboseOption.Description)]
        public bool Verbose { get; set; }

        public int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp(false);
            return 0;
        }

        private static string GetVersion()
            => $"Version {typeof(RootCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}";
    }
}
