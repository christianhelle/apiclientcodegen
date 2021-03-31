using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public interface IDependencyInstaller
    {
        void InstallAutoRest();
        void InstallNSwag();
        string InstallOpenApiGenerator();
        string InstallSwaggerCodegen();
    }
}