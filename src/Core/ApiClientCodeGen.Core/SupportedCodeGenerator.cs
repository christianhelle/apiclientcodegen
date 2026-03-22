using System;

namespace Rapicgen.Core
{
    public enum SupportedCodeGenerator
    {
        NSwag,
        [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
        AutoRest,
        [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
        AutoRestV3,
        Swagger,
        OpenApi,
        NSwagStudio,
        Kiota,
        Refitter
    }
}
