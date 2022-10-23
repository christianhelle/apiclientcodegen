namespace Rapicgen.Core.Options.AutoRest
{
    public class DefaultAutoRestOptions : IAutoRestOptions
    {
        public bool AddCredentials { get; }
        public bool OverrideClientName { get; }
        public bool UseInternalConstructors { get; }
        public SyncMethodOptions SyncMethods { get; }
        public bool UseDateTimeOffset { get; }
        public bool ClientSideValidation { get; } = true;
    }
}