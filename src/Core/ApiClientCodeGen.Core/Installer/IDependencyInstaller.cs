using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.Core.Installer
{
    public interface IDependencyInstaller
    {
        void InstallNSwag();
        string InstallOpenApiGenerator(OpenApiSupportedVersion version = default);
        string InstallSwaggerCodegen();
        void InstallKiota();
        void InstallRefitter();
    }
}
