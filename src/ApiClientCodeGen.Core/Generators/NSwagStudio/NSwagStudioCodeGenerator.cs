using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using Newtonsoft.Json;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio
{
    public class NSwagStudioCodeGenerator : ICodeGenerator
    {
        private readonly string nswagStudioFile;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;

        public NSwagStudioCodeGenerator(string nswagStudioFile, IGeneralOptions options, IProcessLauncher processLauncher)
        {
            this.nswagStudioFile = nswagStudioFile ?? throw new ArgumentNullException(nameof(nswagStudioFile));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
        }

        public string GenerateCode(IProgressReporter pGenerateProgress)
        {
            pGenerateProgress?.Progress(10);

            var command = options.NSwagPath;
            if (!File.Exists(command))
            {
                Trace.WriteLine(command + " does not exist! Retrying with default NSwag.exe path");
                command = PathProvider.GetNSwagPath();
                if (!File.Exists(command))
                    DependencyDownloader.InstallNSwag();
            }

            TryRemoveSwaggerJsonSpec(nswagStudioFile);
            processLauncher.Start(command, $"run \"{nswagStudioFile}\"");
            pGenerateProgress?.Progress(90);
            return null;
        }

        private static void TryRemoveSwaggerJsonSpec(string nswagFile)
        {
            try
            {
                var json = File.ReadAllText(nswagFile);
                dynamic obj = JsonConvert.DeserializeObject(json);
                if (obj.swaggerGenerator.fromSwagger.json == null)
                    return;

                obj.swaggerGenerator.fromSwagger.json = null;
                json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                File.WriteAllText(nswagFile, json);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }
    }
}