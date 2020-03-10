using System;
using System.Diagnostics;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General
{
    public static class PathProvider
    {
        public static string GetJavaPath(string environmentVariable = "JAVA_HOME")
        {
            try
            {
                var javaHome = Environment.GetEnvironmentVariable(environmentVariable);
                var javaExe = Path.Combine(javaHome ?? throw new InvalidOperationException(), "bin\\java.exe");
                return javaExe;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Unable to find JAVA_HOME environment variable");
                return "java";
            }
        }

        public static string GetNpmPath(
            string programFiles = null,
            string programFiles64 = null,
            bool withoutPath = false)
        {
            if (Environment.OSVersion.Platform == PlatformID.MacOSX ||
                Environment.OSVersion.Platform == PlatformID.Unix ||
                withoutPath)
                return "npm";
            
            if (string.IsNullOrWhiteSpace(programFiles))
                programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            if (string.IsNullOrWhiteSpace(programFiles64))
                programFiles64 = programFiles.Replace(" (x86)", string.Empty);

            var npmCommand = Path.Combine(programFiles, "nodejs\\npm.cmd");
            if (!File.Exists(npmCommand))
                npmCommand = Path.Combine(programFiles64, "nodejs\\npm.cmd");
            
            return File.Exists(npmCommand) ? npmCommand : null;
        }

        public static string GetNSwagStudioPath()
            => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Rico Suter\\NSwagStudio\\Win\\NSwag.exe");

        public static string GetNSwagPath()
        {
            if (Environment.OSVersion.Platform == PlatformID.MacOSX ||
                Environment.OSVersion.Platform == PlatformID.Unix)
                return "nswag";

            return Path.Combine(
                NpmHelper.GetPrefixPath(),
                "nswag.cmd");
        }

        public static string GetAutoRestPath(bool withoutPath = false)
        {
            if (Environment.OSVersion.Platform == PlatformID.MacOSX ||
                Environment.OSVersion.Platform == PlatformID.Unix ||
                withoutPath)
                return "autorest";

            return Path.Combine(
                NpmHelper.GetPrefixPath(),
                "autorest.cmd");
        }

        public static string GetSwaggerCodegenPath()
            => Path.Combine(
                Path.GetTempPath(),
                "swagger-codegen-cli.jar");

        public static string GetOpenApiGeneratorPath()
            => Path.Combine(
                Path.GetTempPath(),
                "openapi-generator-cli.jar");
    }
}