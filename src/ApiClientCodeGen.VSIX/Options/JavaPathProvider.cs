using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class JavaPathProvider
    {
        private readonly CustomPathOptions options;

        public JavaPathProvider(IGeneralOptions options)
        {
            this.options = new CustomPathOptions(options);
        }

        public string GetJavaExePath()
        {
            var javaPath = options.JavaPath;
            if (!string.IsNullOrWhiteSpace(javaPath))
            {
                if (File.Exists(javaPath))
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
            }

            if (string.IsNullOrWhiteSpace(options.JavaPath))
                javaPath = PathProvider.GetJavaPath();

            if (File.Exists(javaPath))
                return javaPath;

            throw new NotInstalledException("Unable to find Java");
        }
    }
}
