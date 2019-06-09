using System;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class PathProvider
    {
        public string GetJavaPath()
        {
            var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
            var javaExe = Path.Combine(javaHome, "bin\\java.exe");
            return javaExe;
        }
    }
}
