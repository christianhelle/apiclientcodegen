using System.Threading.Tasks;

namespace Rapicgen.Core.Installer
{
    public interface IDependencyInstaller
    {
        void InstallAutoRest();
        void InstallNSwag();
        string InstallOpenApiGenerator();
        string InstallSwaggerCodegen();
    }
}