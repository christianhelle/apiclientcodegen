using System;
using System.Collections.Generic;

namespace Rapicgen.Core.Generators.AutoRest
{
    [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
    public static class AutoRestConstants
    {
        public static IReadOnlyDictionary<string, string> PropertyGroups { get; }
            = new Dictionary<string, string>
            {
                {"IncludeGeneratorSharedCode", bool.TrueString},
                {
                    "RestoreAdditionalProjectSources",
                    "https://azuresdkartifacts.blob.core.windows.net/azure-sdk-tools/index.json"
                }
            };
    }
}