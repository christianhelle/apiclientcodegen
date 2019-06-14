using System;
using System.Diagnostics;
using System.IO;
using System.Net;
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

        public static string InstallOpenApiGenerator(string path = null)
        {
            const string md5 = "61574C43BEC9B6EDD54E2DD0993F81D5";
            const string url = "http://central.maven.org/maven2/org/openapitools/openapi-generator-cli/4.0.0/openapi-generator-cli-4.0.0.jar";
            const string jar = "openapi-generator-cli.jar";

            return InstallJarFile(path, jar, md5, url);
        }

        public static string InstallSwaggerCodegenCli(string path = null)
        {
            const string md5 = "219F1453FF22482D9E080EFFBFA7FA81";
            const string url = "https://repo1.maven.org/maven2/io/swagger/swagger-codegen-cli/2.4.5/swagger-codegen-cli-2.4.5.jar";
            const string jar = "swagger-codegen-cli.jar";
            
            return InstallJarFile(path, jar, md5, url);
        }

        private static string InstallJarFile(string path, string jar, string md5, string url)
        {
            if (string.IsNullOrWhiteSpace(path))
                path = Path.Combine(Path.GetTempPath(), jar);

            if (!File.Exists(path) || FileHelper.CalculateMd5(path) != md5)
            {
                Trace.WriteLine($"{jar} not found. Attempting to download {jar}");
                new WebClient().DownloadFile(url, path);
                Trace.WriteLine($"{jar} downloaded successfully");
            }

            return path;
        }
    }
}