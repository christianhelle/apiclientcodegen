using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;

namespace ApiClientCodeGen.Tests.Common.Utility
{
    [ExcludeFromCodeCoverage]
    public static class DependencyUninstaller
    {
        public static void UninstallAutoRest()
        {
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var programFiles64 = programFiles.Replace(" (x86)", newValue: string.Empty);

            var npmCommand = Path.Combine(programFiles, "nodejs\\npm.cmd");
            if (!File.Exists(npmCommand))
            {
                npmCommand = Path.Combine(programFiles64, "nodejs\\npm.cmd");
                if (!File.Exists(npmCommand))
                    throw new InvalidOperationException("Unable to find NPM. Please install Node.js");
            }

            new ProcessLauncher().Start(npmCommand, "uninstall -g autorest");
            Logger.Instance.WriteLine("AutoRest uninstalled successfully through NPM");
        }

        public static void UninstallOpenApiGenerator() 
            => File.Delete(Path.Combine(Path.GetTempPath(), "openapi-generator-cli.jar"));

        public static void UninstallSwaggerCodegen() 
            => File.Delete(Path.Combine(Path.GetTempPath(), "swagger-codegen-cli.jar"));
    }
}
