using System;
using System.Threading.Tasks;

namespace Rapicgen.Core.Installer
{
    public interface IDependencyInstaller
    {
        void InstallAutoRest();
        void InstallNSwag();
        string InstallOpenApiGenerator(string? version = null);
        string InstallSwaggerCodegen();
        void InstallKiota();
    }
}