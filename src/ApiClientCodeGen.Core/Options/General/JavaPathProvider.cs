using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General
{
    public class JavaPathProvider
    {
        private readonly IGeneralOptions options;

        public JavaPathProvider(IGeneralOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public string GetJavaExePath()
        {
            var javaPath = options.JavaPath;
            if (!string.IsNullOrWhiteSpace(javaPath) && (File.Exists(javaPath) || javaPath != "java"))
            {
                return javaPath;
            }

            try
            {
                Trace.WriteLine("Checking Java version");
                ProcessHelper.StartProcess("java", "-version");
                return "java";
            }
            catch (Exception e)
            {
                Trace.WriteLine("Java not installed using default settings");
                Trace.WriteLine(e);
            }

            if (string.IsNullOrWhiteSpace(options.JavaPath))
                javaPath = PathProvider.GetJavaPath();

            if (File.Exists(javaPath))
                return javaPath;

            throw new NotInstalledException("Unable to find Java");
        }
    }
}
