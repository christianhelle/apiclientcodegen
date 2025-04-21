using System;
using System.IO;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.Core.External
{
    public static class PathProvider
    {
        public static string GetInstalledJavaPath(string environmentVariable = "JAVA_HOME")
        {
            try
            {
                var javaHome = Environment.GetEnvironmentVariable(environmentVariable);
                if (!string.IsNullOrWhiteSpace(javaHome))
                    return Path.Combine(javaHome, "bin\\java.exe");

                Logger.Instance.WriteLine("Unable to read JAVA_HOME environment variable");
                return "java";
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                return "java";
            }
        }

        public static string GetIncludedJavaPath()
        {
            try
            {
                string codeBase = typeof(PathProvider).Assembly.CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                var jrePath = Path.Combine(Path.GetDirectoryName(path), "JRE");
                return Path.Combine(jrePath, "bin\\java.exe");
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                return "java";
            }
        }

        public static string GetNpmPath(
            string? programFiles = null,
            string? programFiles64 = null,
            bool withoutPath = false)
        {
            if (Environment.OSVersion.Platform is PlatformID.MacOSX or PlatformID.Unix || withoutPath)
                return "npm";

            if (string.IsNullOrWhiteSpace(programFiles))
                programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            if (string.IsNullOrWhiteSpace(programFiles64))
                programFiles64 = programFiles!.Replace(" (x86)", string.Empty);

            var npmCommand = Path.Combine(programFiles, "nodejs\\npm.cmd");
            if (!File.Exists(npmCommand))
                npmCommand = Path.Combine(programFiles64, "nodejs\\npm.cmd");

            return File.Exists(npmCommand) ? npmCommand : string.Empty;
        }

        public static string GetNSwagStudioPath()
            => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Rico Suter\\NSwagStudio\\Win\\NSwag.exe");

        public static string GetNSwagPath(bool withoutPath = false)
        {
            if (Environment.OSVersion.Platform is PlatformID.MacOSX or PlatformID.Unix || withoutPath)
                return "nswag";

            return Path.Combine(
                NpmHelper.GetPrefixPath(),
                "nswag.cmd");
        }

        public static string GetAutoRestPath(bool withoutPath = false)
        {
            if (Environment.OSVersion.Platform is PlatformID.MacOSX or PlatformID.Unix || withoutPath)
                return "autorest";

            return Path.Combine(
                NpmHelper.GetPrefixPath(),
                "autorest.cmd");
        }

        public static string GetSwaggerCodegenPath()
            => Path.Combine(
                Path.GetTempPath(),
                "swagger-codegen-cli.jar");

        public static string GetOpenApiGeneratorPath(
            OpenApiSupportedVersion version = default) 
            => Path.Combine(
                Path.GetTempPath(),
                $"openapi-generator-cli-{version.GetDescription()}.jar");

        public static string GetDotNetPath()
        {
            var programFiles = Environment.Is64BitOperatingSystem
                ? GetProgramFiles64bitPath()
                : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            return Environment.OSVersion.Platform is PlatformID.MacOSX or PlatformID.Unix
                ? "dotnet"
                : Path.Combine(programFiles, "dotnet", "dotnet.exe");
        }

        private static string GetProgramFiles64bitPath()
        {
            var programFiles = Environment.GetEnvironmentVariable("ProgramW6432");
            return string.IsNullOrWhiteSpace(programFiles)
                ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                : programFiles;
        }
    }
}