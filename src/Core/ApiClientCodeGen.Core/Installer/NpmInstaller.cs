using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public class NpmInstaller : INpmInstaller
    {
        private readonly IProcessLauncher processLauncher;

        public NpmInstaller(IProcessLauncher processLauncher)
        {
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
        }
        
        public async Task InstallNpmPackage(string packageName)
        {
            Trace.WriteLine($"Attempting to install {packageName} through NPM");

            var npmPath = NpmHelper.GetNpmPath();
            await Task.Run(
                () => processLauncher.Start(
                    npmPath,
                    $"install -g {packageName}"));

            Trace.WriteLine($"{packageName} installed successfully through NPM");
        }
    }
}