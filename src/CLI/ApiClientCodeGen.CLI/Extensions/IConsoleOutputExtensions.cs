using System;
using Rapicgen.Core.Logging;

namespace Rapicgen.CLI.Extensions
{
    public static class IConsoleOutputExtensions
    {
        public static void WriteSignature(this IConsoleOutput console)
        {
            console.WriteLine(Environment.NewLine);
            console.WriteMarkup("[bold cyan]" + new string('═', 67) + "[/]");
            console.WriteLine(Environment.NewLine);
            console.WriteMarkup("[bold yellow]  Do you find this tool useful?[/]");
            console.WriteLine(Environment.NewLine);
            console.WriteMarkup("[bold blue]  https://www.buymeacoffee.com/christianhelle[/]");
            console.WriteLine(Environment.NewLine);
            console.WriteLine(Environment.NewLine);
            console.WriteMarkup("[bold yellow]  Does this tool not work or does it lack something you need?[/]");
            console.WriteLine(Environment.NewLine);
            console.WriteMarkup("[bold blue]  https://github.com/christianhelle/apiclientcodegen/issues[/]");
            console.WriteLine(Environment.NewLine);
            console.WriteMarkup("[bold cyan]" + new string('═', 67) + "[/]");
            console.WriteLine(Environment.NewLine);
        }
    }
}