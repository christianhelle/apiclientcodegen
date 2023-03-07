using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ApiClientCodeGen.VSMac.CustomTools.OpenApi;
using ApiClientCodeGen.VSMac.CustomTools.Swagger;
using ApiClientCodeGen.VSMac.Logging;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;
using ProgressReporter = ApiClientCodeGen.VSMac.Logging.ProgressReporter;

namespace ApiClientCodeGen.VSMac.CustomTools
{
    public abstract class BaseSingleFileCustomTool : ISingleFileCustomTool
    {
        public async Task Generate(
            ProgressMonitor monitor,
            ProjectFile file,
            SingleFileCustomToolResult result)
        {
            var generatorName = GetGeneratorName();
            Logger.Instance.TrackFeatureUsage(generatorName, "VSMac");
            Bootstrapper.Initialize();

            using var traceListener = new DisposableTraceListener(
                new LoggingServiceTraceListener(
                    new ProgressMonitorLoggingService(
                        monitor, 
                        "Generating code...")));

            var outputFile = await GenerateCodeAsync(monitor, file, result);
            TryLogOutputFileSize(outputFile);

            Trace.WriteLine(Environment.NewLine);
            Trace.WriteLine("###################################################################");
            Trace.WriteLine("#  Do you find this tool useful?                                  #");
            Trace.WriteLine("#  https://www.buymeacoffee.com/christianhelle                    #");
            Trace.WriteLine("#                                                                 #");
            Trace.WriteLine("#  Does this tool not work or does it lack something you need?    #");
            Trace.WriteLine("#  https://github.com/christianhelle/apiclientcodegen/issues      #");
            Trace.WriteLine("###################################################################");
        }

        private string GetGeneratorName()
        {
            string generatorName;
            if (GetType() == typeof(OpenApiSingleFileCustomTool))
                generatorName = "OpenAPI Generator";
            else if (GetType() == typeof(SwaggerSingleFileCustomTool))
                generatorName = "Swagger Codegen CLI";
            else
                generatorName = GetType().Name.Replace("SingleFileCustomTool", string.Empty);
            return generatorName;
        }

        private async Task<FilePath> GenerateCodeAsync(
            ProgressMonitor monitor,
            ProjectFile file,
            SingleFileCustomToolResult result)
        {
            var swaggerFile = file.FilePath;
            var outputFile = swaggerFile.ChangeExtension(".cs");
            result.GeneratedFilePath = outputFile;

            var customToolNamespace = file.CustomToolNamespace;
            if (string.IsNullOrWhiteSpace(customToolNamespace))
                customToolNamespace = CustomToolService.GetFileNamespace(file, outputFile);

            var generator = GetCodeGenerator(swaggerFile, customToolNamespace);
            var progressReporter = new ProgressReporter(monitor);
            var contents = await Task.Run(() => generator.GenerateCode(progressReporter));
            await File.WriteAllTextAsync(outputFile, contents);
            return outputFile;
        }

        private static void TryLogOutputFileSize(FilePath outputFile)
        {
            try
            {
                var fileInfo = new FileInfo(outputFile.FullPath);
                var length = fileInfo.Length.ToString();
                Trace.WriteLine($"{Environment.NewLine}Output file size: {length}");
            }
            catch
            {
                // Ignore
            }
        }

        protected abstract ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace);
    }
}