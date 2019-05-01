using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio
{
    public class NSwagStudioCodeGenerator : ICodeGenerator
    {
        private readonly string nswagStudioFile;

        public NSwagStudioCodeGenerator(string nswagStudioFile)
        {
            this.nswagStudioFile = nswagStudioFile ?? throw new ArgumentNullException(nameof(nswagStudioFile));
        }

        public string GenerateCode(IVsGeneratorProgress pGenerateProgress)
        {
            pGenerateProgress?.Progress(10);

            var command = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Rico Suter\\NSwagStudio\\Win\\NSwag.exe");

            if (!File.Exists(command))
                throw new NotInstalledException("NSwag not installed. Please install NSwagStudio");

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
