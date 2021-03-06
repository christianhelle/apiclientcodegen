using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public interface IDependencyInstaller
    {
        Task InstallAutoRest();
        Task InstallNSwag();
        Task<string> InstallOpenApiGenerator();
        Task<string> InstallSwaggerCodegen();
    }
}