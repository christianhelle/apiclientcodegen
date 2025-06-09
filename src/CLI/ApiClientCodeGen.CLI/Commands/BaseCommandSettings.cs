using System.ComponentModel;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands
{
    public class BaseCommandSettings : CommandSettings
    {
        [CommandOption("-v|--verbose")]
        [Description("Show verbose output")]
        public bool Verbose { get; set; }

        [CommandOption("-n|--no-logging")]
        [Description("Disables Analytics and Error Reporting")]
        public bool SkipLogging { get; set; }
    }
}