using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest
{
    public static class AutoRestConstants
    {
        public static readonly Dictionary<string, object> PropertyGroups
            = new Dictionary<string, object>
            {
                {"IncludeGeneratorSharedCode", true},
                {
                    "RestoreAdditionalProjectSources",
                    "https://azuresdkartifacts.blob.core.windows.net/azure-sdk-tools/index.json"
                }
            };
    }
}