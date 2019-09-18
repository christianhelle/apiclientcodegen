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
            const string md5 = "45F0AEDA24983AC998878C4D8516CF48";
            const string url = "http://central.maven.org/maven2/org/openapitools/openapi-generator-cli/4.1.2/openapi-generator-cli-4.1.2.jar";
            const string jar = "openapi-generator-cli.jar";

            return InstallJarFile(path, jar, md5, url);
        }

        public static string InstallSwaggerCodegenCli(string path = null)
        {
            const string md5 = "89E1C5F578CC0B7A5D430CDF8210AC44";
            const string url = "https://repo1.maven.org/maven2/io/swagger/codegen/v3/swagger-codegen-cli/3.0.11/swagger-codegen-cli-3.0.11.jar";
            const string jar = "swagger-codegen-cli.jar";
            
            return InstallJarFile(path, jar, md5, url);
        }

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