using System.Collections.Generic;

namespace Rapicgen.Core.Generators.AutoRest
{
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