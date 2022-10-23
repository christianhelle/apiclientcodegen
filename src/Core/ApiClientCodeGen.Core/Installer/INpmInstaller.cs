using System.Threading.Tasks;

namespace Rapicgen.Core.Installer
{
    public interface INpmInstaller
    {
        void InstallNpmPackage(string packageName);
    }
}