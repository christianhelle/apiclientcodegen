using System;
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
        private readonly INpmInstaller npm;

        public DependencyInstaller(INpmInstaller npm)
        {
            this.npm = npm ?? throw new ArgumentNullException(nameof(npm));
        }

        public Task InstallAutoRest()
        {
            return npm.InstallNpmPackage("autorest");
        }

        public Task InstallNSwag()
        {
            return npm.InstallNpmPackage("nswag");
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