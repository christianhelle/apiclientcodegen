using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest
{
    public class AutoRestCSharpCodeGenerator : ICodeGenerator
    {
        private readonly IAutoRestOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IOpenApiDocumentFactory documentFactory;
        private static readonly object SyncLock = new object();

        public string SwaggerFile { get; }
        public string DefaultNamespace { get; }

        public AutoRestCSharpCodeGenerator(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory)
        {
            SwaggerFile = swaggerFile;
            DefaultNamespace = defaultNamespace;
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher;
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
        }

        [SuppressMessage(
            "Usage",
            "VSTHRD002:Avoid problematic synchronous waits",
            Justification = "This is code is called from an old pre-TPL interface")]
        public string GenerateCode(IProgressReporter pGenerateProgress)
        {
            lock (SyncLock)
                return OnGenerateCode(pGenerateProgress);
        }

        private string OnGenerateCode(IProgressReporter pGenerateProgress)
        {
            try
            {
                pGenerateProgress.Progress(10);

                var command = PathProvider.GetAutoRestPath();
                pGenerateProgress.Progress(30);

                DependencyDownloader.InstallAutoRest();
                pGenerateProgress.Progress(50);

                var document = documentFactory.GetDocumentAsync(SwaggerFile).GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(document.OpenApi) &&
                    Version.TryParse(document.OpenApi, out var openApiVersion) &&
                    openApiVersion > Version.Parse("3.0.0"))
                {
                    var outputFolder = Path.Combine(
                        Path.GetDirectoryName(SwaggerFile) ?? throw new InvalidOperationException(),
                        Guid.NewGuid().ToString("N"),
                        "TempApiClient");

                    processLauncher.Start(command, GetArguments(outputFolder), Path.GetDirectoryName(SwaggerFile));
                    pGenerateProgress.Progress(80);

                    return CSharpFileMerger.MergeFiles(outputFolder);
                }
                else
                {
                    var outputFile = Path.GetTempFileName();
                    var arguments = GetLegacyArguments(outputFile);
                    try
                    {
                        processLauncher.Start(
                            command,
                            arguments,
                            Path.GetDirectoryName(SwaggerFile));
                    }
                    catch (ProcessLaunchException)
                    {
                        processLauncher.Start(
                            command,
                            arguments.Replace("--version=", "--version "),
                            Path.GetDirectoryName(SwaggerFile));
                    }
                    finally
                    {
                        pGenerateProgress.Progress(80);
                    }

                    return FileHelper.ReadThenDelete(outputFile);
                }
            }
            finally
            {
                pGenerateProgress.Progress(90);
            }
        }

        private string GetLegacyArguments(string outputFile)
        {
            return AppendCommonArguments(
                "--version=2.0.4417 --csharp " +
                $"--input-file=\"{SwaggerFile}\" " +
                $"--output-file=\"{outputFile}\" " +
                $"--namespace=\"{DefaultNamespace}\" ");
        }

        private string GetArguments(string outputFolder)
        {
            return AppendCommonArguments(
                "--use:@autorest/csharp@3.0.0-beta.20210218.1 " +
                $"--input-file=\"{SwaggerFile}\" " +
                $"--output-folder=\"{outputFolder}\" " +
                $"--namespace=\"{DefaultNamespace}\" ");
        }

        private string AppendCommonArguments(string args)
        {
            if (options.AddCredentials)
                args += "--add-credentials ";

            args += $"--client-side-validation=\"{options.ClientSideValidation}\" ";
            args += $"--sync-methods=\"{options.SyncMethods}\" ";

            if (options.UseDateTimeOffset)
                args += "--use-datetimeoffset ";

            if (options.UseInternalConstructors)
                args += " --use-internal-constructors ";

            if (!options.OverrideClientName)
                return args;

            var file = new FileInfo(SwaggerFile);
            var name = file.Name
                .Replace(" ", string.Empty)
                .Replace(file.Extension, string.Empty);

            args += $" --override-client-name=\"{name}\"";
            return args;
        }
    }
}