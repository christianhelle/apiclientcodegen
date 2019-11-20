using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;

namespace ApiClientCodeGen.CLI.Options
{
    public class AutoRestOptions : IAutoRestOptions
    {
        public bool AddCredentials { get; set; }
        public bool OverrideClientName { get; set; }
        public bool UseInternalConstructors { get; set; }
        public SyncMethodOptions SyncMethods { get; set; }
        public bool UseDateTimeOffset { get; set; }
        public bool ClientSideValidation { get; set; }
    }
}