using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;

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
                    throw new InvalidOperationException("Unable to find NPM. Please install Node.js");
            }

            new ProcessLauncher().Start(npmCommand, "install -g autorest");
            Trace.WriteLine("AutoRest installed successfully through NPM");
        }

        public static string InstallOpenApiGenerator(string path = null)
            => InstallJarFile(
                path,
                "openapi-generator-cli.jar",
                Resource.OpenApiGenerator_MD5,
                Resource.OpenApiGenerator_DownloadUrl);

        public static string InstallSwaggerCodegenCli(string path = null)
            => InstallJarFile(
                path,
                "swagger-codegen-cli.jar",
                Resource.SwaggerCodegenCli_MD5,
                Resource.SwaggerCodegenCli_DownloadUrl);

        private static string InstallJarFile(string path, string jar, string md5, string url)
        {
            if (string.IsNullOrWhiteSpace(path))
                path = Path.Combine(Path.GetTempPath(), jar);

            if (!File.Exists(path) || FileHelper.CalculateChecksum(path) != md5)
            {
                Trace.WriteLine($"{jar} not found. Attempting to download {jar}");
                new WebClient().DownloadFile(url, path);
                Trace.WriteLine($"{jar} downloaded successfully");
            }

            return path;
        }
    }
}