using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public interface IDependencyInstaller
    {
        Task InstallAutoRest();
        Task InstallNSwag();
        Task InstallOpenApiGenerator();
        Task InstallSwaggerCodegen();
    }

    public class DependencyInstaller : IDependencyInstaller
    {
        public Task InstallAutoRest()
        {
            throw new System.NotImplementedException();
        }

        public Task InstallNSwag()
        {
            throw new System.NotImplementedException();
        }

        public Task InstallOpenApiGenerator()
        {
            throw new System.NotImplementedException();
        }

        public Task InstallSwaggerCodegen()
        {
            throw new System.NotImplementedException();
        }
    }
}