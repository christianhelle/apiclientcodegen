﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.AutoRest;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.Generators.AutoRest
{
    public class AutoRestCSharpCodeGenerator : ICodeGenerator
    {
        private readonly IProcessLauncher processLauncher;
        private readonly IOpenApiDocumentFactory documentFactory;
        private readonly IDependencyInstaller dependencyInstaller;
        private readonly IAutoRestArgumentProvider argumentProvider;
        private static readonly object SyncLock = new();

        public string SwaggerFile { get; }
        public string DefaultNamespace { get; }

        public AutoRestCSharpCodeGenerator(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller,
            IAutoRestArgumentProvider? argumentProvider = null)
        {
            SwaggerFile = swaggerFile;
            DefaultNamespace = defaultNamespace;
            this.processLauncher = processLauncher;
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
            this.argumentProvider = argumentProvider ?? new AutoRestArgumentProvider(options);
        }

        public string GenerateCode(IProgressReporter? pGenerateProgress)
        {
            lock (SyncLock)
                return OnGenerateCode(pGenerateProgress);
        }

        [SuppressMessage(
            "Usage",
            "VSTHRD002:Avoid problematic synchronous waits",
            Justification = "This is code is called from an old pre-TPL interface")]
        private string OnGenerateCode(IProgressReporter? pGenerateProgress)
        {
            try
            {
                pGenerateProgress?.Progress(10);

                var command = PathProvider.GetAutoRestPath();
                pGenerateProgress?.Progress(30);

                dependencyInstaller.InstallAutoRest();
                pGenerateProgress?.Progress(50);

                var document = documentFactory.GetDocumentAsync(SwaggerFile).GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(document.OpenApi) &&
                    Version.TryParse(document.OpenApi, out var openApiVersion) &&
                    openApiVersion > Version.Parse("3.0.0"))
                {
                    var outputFolder = Path.Combine(
                        Path.GetDirectoryName(SwaggerFile) ?? throw new InvalidOperationException(),
                        Guid.NewGuid().ToString("N"),
                        "TempApiClient");

                    if (!Directory.Exists(outputFolder))
                        Directory.CreateDirectory(outputFolder);
                    
                    var arguments = argumentProvider.GetArguments(
                        outputFolder,
                        SwaggerFile,
                        DefaultNamespace);
                    
                    using var context = new DependencyContext("AutoRest", $"{command} {arguments}");
                    processLauncher.Start(command, arguments, Path.GetDirectoryName(SwaggerFile));
                    context.Succeeded();

                    pGenerateProgress?.Progress(80);

                    return CSharpFileMerger.MergeFilesAndDeleteSource(outputFolder);
                }
                else
                {
                    var outputFile = FileHelper.CreateRandomFile();
                    var arguments = argumentProvider.GetLegacyArguments(
                        outputFile,
                        SwaggerFile,
                        DefaultNamespace);

                    try
                    {
                        using var context = new DependencyContext("AutoRest", $"{command} {arguments}");
                        processLauncher.Start(command, arguments, Path.GetDirectoryName(SwaggerFile));
                        context.Succeeded();
                    }
                    catch (ProcessLaunchException)
                    {
                        arguments = arguments.Replace("--version=", "--version ");
                        using var context = new DependencyContext("AutoRest", $"{command} {arguments}");
                        processLauncher.Start(command, arguments, Path.GetDirectoryName(SwaggerFile));
                        context.Succeeded();
                    }
                    finally
                    {
                        pGenerateProgress?.Progress(80);
                    }

                    return FileHelper.ReadThenDelete(outputFile);
                }
            }
            finally
            {
                pGenerateProgress?.Progress(90);
            }
        }
    }
}