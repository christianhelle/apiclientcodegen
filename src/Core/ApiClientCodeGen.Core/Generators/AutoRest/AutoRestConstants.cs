using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest
{
    public static class AutoRestConstants
    {
        public static readonly Dictionary<string, string> PropertyGroups
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