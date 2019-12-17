using System;
using System.Diagnostics;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General
{
    public static class PathProvider
    {
        public static string GetJavaPath()
        {
            try
            {
                var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
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

        public static string GetNpmPath()
        {
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var programFiles64 = programFiles.Replace(" (x86)", newValue: string.Empty);

            var npmCommand = Path.Combine(programFiles, "nodejs\\npm.cmd");
            if (File.Exists(npmCommand))
                return npmCommand;

            npmCommand = Path.Combine(programFiles64, "nodejs\\npm.cmd");
            return !File.Exists(npmCommand) ? null : npmCommand;
        }
        
        public static string GetNSwagStudioPath()
            => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Rico Suter\\NSwagStudio\\Win\\NSwag.exe");

        public static string GetNSwagPath()
            => Path.Combine(
                NpmHelper.GetPrefixPath(),
                "nswag.cmd");

        public static string GetAutoRestPath()
            => Path.Combine(
                NpmHelper.GetPrefixPath(),
                "autorest.cmd");

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
