using System;
using System.Linq;

namespace ApiClientCodeGen.CLI
{
    public static class VerboseOption
    {
        public const string Template = "-v|--verbose";
        public const string Description = "Show verbose output";

        public static bool Parse(params string[] args)
            => args.Any(s
                => s.Equals("-v", StringComparison.OrdinalIgnoreCase)
                   || s.Equals("--verbose", StringComparison.OrdinalIgnoreCase));

    }
}