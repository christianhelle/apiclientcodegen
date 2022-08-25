using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using Newtonsoft.Json;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio
{
    public class NSwagStudioCodeGenerator : ICodeGenerator
    {
        private readonly string nswagStudioFile;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;
        private static readonly object SyncLock = new object();
        private readonly bool forceDownload;

        public NSwagStudioCodeGenerator(
            string nswagStudioFile,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller,
            bool forceDownload = false)
        {
            this.nswagStudioFile = nswagStudioFile ?? throw new ArgumentNullException(nameof(nswagStudioFile));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
            this.forceDownload = forceDownload;
        }

        public string GenerateCode(IProgressReporter? pGenerateProgress)
        {
            Logger.Instance.TrackFeatureUsage("Generate NSwag Studio output");
            
            pGenerateProgress?.Progress(10);

            lock (SyncLock)
            {
                TryRemoveSwaggerJsonSpec(nswagStudioFile);
                pGenerateProgress?.Progress(25);

                var command = GetNSwagPath();
                pGenerateProgress?.Progress(50);

                var arguments = $"run \"{nswagStudioFile}\"";

                try
                {
                    processLauncher.Start(command, arguments, Path.GetDirectoryName(nswagStudioFile)!); 
                }
                catch
                {
                    Logger.Instance.TrackDependencyFailure("NSwag Studio");
                    throw;
                }
            }
            
            pGenerateProgress?.Progress(100);
            return string.Empty;
        }

        public string GetNSwagPath()
        {
            var command = options.NSwagPath;
            if (!string.IsNullOrWhiteSpace(command) && File.Exists(command) && !forceDownload)
                return command;

            Trace.WriteLine(
                forceDownload
                    ? "Downloading NSwag using NPM"
                    : $"{command} could not be found in specified path! Retrying with default NSwag.exe path");

            command = PathProvider.GetNSwagPath();
            if (!File.Exists(command) || forceDownload)
                dependencyInstaller.InstallNSwag();

            return command;
        }

        private static void TryRemoveSwaggerJsonSpec(string nswagFile)
        {
            try
            {
                var json = File.ReadAllText(nswagFile);
                dynamic obj = JsonConvert.DeserializeObject(json);

                if (obj?.swaggerGenerator?.fromSwagger?.json == null && 
                    obj?.documentGenerator?.fromDocument?.json == null)
                    return;

                if (obj?.swaggerGenerator?.fromSwagger?.json != null)
                    obj.swaggerGenerator.fromSwagger.json = null;

                if (obj?.documentGenerator?.fromDocument?.json != null)
                    obj.documentGenerator.fromDocument.json = null;

                json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                File.WriteAllText(nswagFile, json);
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Trace.TraceError(e.ToString());
            }
        }
    }
}