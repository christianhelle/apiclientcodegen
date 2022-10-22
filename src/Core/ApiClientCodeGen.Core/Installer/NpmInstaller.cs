using System;
using System.Diagnostics;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.Installer
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

            using var context = new DependencyContext($"npm install -g {packageName}");
            processLauncher.Start(PathProvider.GetNpmPath(), $"install -g {packageName}");
            context.Succeeded();

            Trace.WriteLine($"{packageName} installed successfully through NPM");
        }
    }
}