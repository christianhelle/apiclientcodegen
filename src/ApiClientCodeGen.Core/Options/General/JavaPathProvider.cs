using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General
{
    public class JavaPathProvider
    {
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;

        public JavaPathProvider(IGeneralOptions options, IProcessLauncher processLauncher)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
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
                processLauncher.Start("java", "-version");
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

            throw new FileNotFoundException("Unable to find Java");
        }
    }
}
