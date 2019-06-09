using System;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public static class PathProvider
    {
        public static string GetJavaPath()
        {
            var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
            var javaExe = Path.Combine(javaHome, "bin\\java.exe");
            return javaExe;
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

        public static string GetNSwagPath()
            => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Rico Suter\\NSwagStudio\\Win\\NSwag.exe");
    }
}
