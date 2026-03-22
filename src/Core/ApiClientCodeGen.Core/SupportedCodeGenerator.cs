using System;

namespace Rapicgen.Core
{
    public enum SupportedCodeGenerator
    {
        NSwag = 0,
        [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
        AutoRest = 1,
        [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
        AutoRestV3 = 2,
        Swagger = 3,
        OpenApi = 4,
        NSwagStudio = 5,
        Kiota = 6,
        Refitter = 7
    }
}
