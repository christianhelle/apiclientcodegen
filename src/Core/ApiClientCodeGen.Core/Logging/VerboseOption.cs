using System;
using System.Collections.Generic;
using System.Linq;

namespace Rapicgen.Core.Logging
{
    public class VerboseOption : IVerboseOptions
    {
        public const string Template = "-v|--verbose";
        public const string Description = "Show verbose output";

        public VerboseOption(IEnumerable<string> args)
        {
            Enabled = args.Any(s
                => s.Equals("-v", StringComparison.OrdinalIgnoreCase)
                   || s.Equals("--verbose", StringComparison.OrdinalIgnoreCase));
        }

        public bool Enabled { get; }
    }
}