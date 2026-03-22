using System;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.Core.Installer
{
    public interface IDependencyInstaller
    {
        [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
        void InstallAutoRest();
        void InstallNSwag();
        string InstallOpenApiGenerator(OpenApiSupportedVersion version = default);
        string InstallSwaggerCodegen();
        void InstallKiota();
        void InstallRefitter();
    }
}
