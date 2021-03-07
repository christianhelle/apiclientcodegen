using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public interface INpmInstaller
    {
        void InstallNpmPackage(string packageName);
    }
}