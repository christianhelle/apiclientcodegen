using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
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
        
        public Task InstallNpmPackage(string packageName) 
            => Task.Run(() => StartProcess(packageName));

        private Task StartProcess(string packageName)
        {
            try
            {
                Trace.WriteLine($"Attempting to install {packageName} through NPM");
                
                processLauncher.Start(
                    PathProvider.GetNpmPath(),
                    $"install -g {packageName}");
                
                Trace.WriteLine($"{packageName} installed successfully through NPM");
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Trace.WriteLine($"NPM {packageName} installation failed");
                return Task.FromException(e);
            }
        }
    }
}