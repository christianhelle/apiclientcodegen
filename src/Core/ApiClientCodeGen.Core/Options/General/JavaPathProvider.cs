using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;

namespace Rapicgen.Core.Options.General
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
            if (!string.IsNullOrWhiteSpace(javaPath) &&
                (File.Exists(javaPath) || javaPath != "java") &&
                CheckJavaVersion(javaPath)) return javaPath;

            if (CheckJavaVersion("java"))
                return "java";

            if (string.IsNullOrWhiteSpace(options.JavaPath))
                javaPath = PathProvider.GetJavaPath();

            return javaPath;
        }

        [ExcludeFromCodeCoverage]
        private bool CheckJavaVersion(string javaPath)
        {
            try
            {
                Trace.WriteLine("Checking Java version");
                processLauncher.Start(javaPath, "-version");
                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Trace.WriteLine($"Unable to start Java from path: {javaPath}");
                Trace.WriteLine(e);
            }

            return false;
        }
    }
}