using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public class NpmInstaller : INpmInstaller
    {
        private readonly IProcessLauncher processLauncher;

        public NpmInstaller(IProcessLauncher processLauncher)
        {
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
        }

        public void InstallNpmPackage(string packageName)
        {
            TraceLogger.WriteLine($"Attempting to install {packageName} through NPM");

            processLauncher.Start(
                PathProvider.GetNpmPath(),
                $"install -g {packageName}");

            TraceLogger.WriteLine($"{packageName} installed successfully through NPM");
        }
    }
}