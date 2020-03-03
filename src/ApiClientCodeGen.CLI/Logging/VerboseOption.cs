using System;
using System.Linq;

namespace ApiClientCodeGen.CLI.Logging
{
    public static class VerboseOption
    {
        public const string Template = "-v|--verbose";
        public const string Description = "Show verbose output";

        public static bool Parse(params string[] args)
        {
            Enabled = args.Any(s
                => s.Equals("-v", StringComparison.OrdinalIgnoreCase)
                   || s.Equals("--verbose", StringComparison.OrdinalIgnoreCase));
            return Enabled;
        }

        public static bool Enabled { get; set; }
    }
}