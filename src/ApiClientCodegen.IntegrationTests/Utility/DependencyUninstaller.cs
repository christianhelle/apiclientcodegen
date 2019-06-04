using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility
{
    public static class DependencyUninstaller
    {
        public static void UninstallAutoRest()
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

            ProcessHelper.StartProcess(npmCommand, "uninstall -g autorest");
            Trace.WriteLine("AutoRest installed successfully through NPM");
        }

        public static void UninstallOpenApiGenerator() 
            => File.Delete(Path.Combine(Path.GetTempPath(), "openapi-generator-cli.jar"));

        public static void UninstallSwaggerCodegen() 
            => File.Delete(Path.Combine(Path.GetTempPath(), "swagger-codegen-cli.jar"));
    }
}
