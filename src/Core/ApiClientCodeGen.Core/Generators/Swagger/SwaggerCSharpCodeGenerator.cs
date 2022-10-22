﻿using System;
using System.Diagnostics;
using System.IO;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.Generators.Swagger
{
    public class SwaggerCSharpCodeGenerator : ICodeGenerator
    {
        private readonly string defaultNamespace;
        private readonly JavaPathProvider javaPathProvider;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;
        private readonly string swaggerFile;

        public SwaggerCSharpCodeGenerator(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
            javaPathProvider = new JavaPathProvider(options, processLauncher);
        }

        public string GenerateCode(IProgressReporter? pGenerateProgress)
        {
            string arguments = null!;
            try
            {
                pGenerateProgress?.Progress(10);

                var jarFile = options.SwaggerCodegenPath;
                if (!File.Exists(jarFile))
                {
                    Trace.WriteLine(jarFile + " does not exist");
                    jarFile = dependencyInstaller.InstallSwaggerCodegen();
                }

                pGenerateProgress?.Progress(30);

                var output = Path.Combine(
                    Path.GetDirectoryName(swaggerFile) ?? throw new InvalidOperationException(),
                    Guid.NewGuid().ToString("N"),
                    "TempApiClient");

                Directory.CreateDirectory(output);
                pGenerateProgress?.Progress(40);

                arguments = $"-jar \"{jarFile}\" generate " +
                            "-l csharp " +
                            $"--input-spec \"{swaggerFile}\" " +
                            $"--output \"{output}\" " +
                            "-DapiTests=false -DmodelTests=false " +
                            $"-DpackageName={defaultNamespace} ";

                var java = javaPathProvider.GetJavaExePath();
                using var context = new DependencyContext("Swagger Codegen CLI", $"{java} {arguments}");
                processLauncher.Start(java, arguments);
                context.Succeeded();
                
                pGenerateProgress?.Progress(80);

                return CSharpFileMerger.MergeFilesAndDeleteSource(output);
            }
            finally
            {
                pGenerateProgress?.Progress(90);
            }
        }
    }
}