using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    public static class DependencyDownloader
    {
        public static void InstallAutoRest()
        {
            Trace.WriteLine("AutoRest not installed. Attempting to install through NPM");

            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var programFiles64 = programFiles.Replace(" (x86)", newValue: string.Empty);

            var npmCommand = Path.Combine(programFiles, "nodejs\\npm.cmd");
            if (!File.Exists(npmCommand))
            {
                npmCommand = Path.Combine(programFiles64, "nodejs\\npm.cmd");
                if (!File.Exists(npmCommand))
                    throw new NotInstalledException("Unable to find NPM. Please install Node.js");
            }

            ProcessHelper.StartProcess(npmCommand, "install -g autorest");
            Trace.WriteLine("AutoRest installed successfully through NPM");
        }
    }
}