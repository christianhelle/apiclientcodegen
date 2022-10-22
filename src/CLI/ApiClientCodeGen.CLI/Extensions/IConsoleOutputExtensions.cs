using System;
using Rapicgen.Core.Logging;

namespace Rapicgen.CLI.Extensions
{
    public static class IConsoleOutputExtensions
    {
        public static void WriteSignature(this IConsoleOutput console)
        {
            console.WriteLine(Environment.NewLine);
            console.WriteLine("###################################################################");
            console.WriteLine("#  Do you find this tool useful?                                  #");
            console.WriteLine("#  https://www.buymeacoffee.com/christianhelle                    #");
            console.WriteLine("#                                                                 #");
            console.WriteLine("#  Does this tool not work or does it lack something you need?    #");
            console.WriteLine("#  https://github.com/christianhelle/apiclientcodegen/issues      #");
            console.WriteLine("###################################################################");
        }
    }
}