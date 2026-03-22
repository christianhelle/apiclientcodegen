using System;

namespace Rapicgen.Core.Options.AutoRest
{
    [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
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