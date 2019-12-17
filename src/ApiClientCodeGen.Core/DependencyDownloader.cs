using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    public static class DependencyDownloader
    {
        public static void InstallAutoRest() => InstallNpmPackage("autorest");

        public static void InstallNSwag() => InstallNpmPackage("nswag");

        private static void InstallNpmPackage(string packageName)
        {
            Trace.WriteLine($"Attempting to install {packageName} through NPM");
            
            var processLauncher = new ProcessLauncher();
            var npmPath = NpmHelper.GetNpmPath();
            processLauncher.Start(
                npmPath,
                $"install -g {packageName}");

            Trace.WriteLine($"{packageName} installed successfully through NPM");
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