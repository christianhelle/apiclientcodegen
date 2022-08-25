using System;
using System.Diagnostics;
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
            Trace.WriteLine($"Attempting to install {packageName} through NPM");

            try
            {
                processLauncher.Start(
                    PathProvider.GetNpmPath(),
                    $"install -g {packageName}");

                Trace.WriteLine($"{packageName} installed successfully through NPM");
            }
            catch
            {
                Logger.Instance.TrackDependencyFailure($"npm install -g {packageName}");
                throw;
            }
        }
    }
}