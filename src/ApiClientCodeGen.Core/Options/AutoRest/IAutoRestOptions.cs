namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest
{
    public interface IAutoRestOptions
    {
        bool AddCredentials { get; }
        bool OverrideClientName { get; }
        bool UseInternalConstructors { get; }
        SyncMethodOptions SyncMethods { get; }
        bool UseDateTimeOffset { get; }
        bool ClientSideValidation { get; }
    }
}