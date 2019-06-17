using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio
{
    public class NSwagStudioCodeGenerator : ICodeGenerator
    {
        private readonly string nswagStudioFile;
        private readonly CustomPathOptions options;

        public NSwagStudioCodeGenerator(string nswagStudioFile, IGeneralOptions options)
        {
            this.nswagStudioFile = nswagStudioFile ?? throw new ArgumentNullException(nameof(nswagStudioFile));
            this.options = new CustomPathOptions(options ?? throw new ArgumentNullException(nameof(options)));
        }

        public string GenerateCode(IVsGeneratorProgress pGenerateProgress)
        {
            pGenerateProgress?.Progress(10);

            var command = options.NSwagPath;
            if (!File.Exists(command))
            {
                Trace.WriteLine(command + " does not exist! Retrying with default NSwag.exe path");
                command = PathProvider.GetNSwagPath();
                if (!File.Exists(command))
                    throw new NotInstalledException("NSwag not installed. Please install NSwagStudio");
            }

            TryRemoveSwaggerJsonSpec(nswagStudioFile);
            ProcessHelper.StartProcess(command, $"run \"{nswagStudioFile}\"");
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
