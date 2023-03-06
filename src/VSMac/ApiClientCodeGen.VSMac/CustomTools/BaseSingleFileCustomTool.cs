using System;
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
            string generatorName;
            if (GetType() == typeof(OpenApiSingleFileCustomTool))
                generatorName = "OpenAPI Generator";
            else if (GetType() == typeof(SwaggerSingleFileCustomTool))
                generatorName = "Swagger Codegen CLI";
            else
                generatorName = GetType().Name.Replace("SingleFileCustomTool", string.Empty);

            Logger.Instance.TrackFeatureUsage(generatorName, "VSMac");

            Bootstrapper.Initialize();

            var swaggerFile = file.FilePath;
            var outputFile = swaggerFile.ChangeExtension(".cs");
            result.GeneratedFilePath = outputFile;

            using var traceListener = new DisposableTraceListener(
                new LoggingServiceTraceListener(
                    new ProgressMonitorLoggingService(monitor, "Generating code...")));

            var customToolNamespace = file.CustomToolNamespace;
            if (string.IsNullOrWhiteSpace(customToolNamespace))
                customToolNamespace = CustomToolService.GetFileNamespace(file, outputFile);

            var generator = GetCodeGenerator(swaggerFile, customToolNamespace);
            var progressReporter = new ProgressReporter(monitor);
            var contents = await Task.Run(() => generator.GenerateCode(progressReporter));
            await File.WriteAllTextAsync(outputFile, contents).ConfigureAwait(false);

            var fileInfo = new FileInfo(outputFile.FullPath);
            var length = fileInfo.Length.ToString();
            Logger.Instance.WriteLine($"{Environment.NewLine}Output file size: {length}");

            Logger.Instance.WriteLine(Environment.NewLine);
            Logger.Instance.WriteLine("###################################################################");
            Logger.Instance.WriteLine("#  Do you find this tool useful?                                  #");
            Logger.Instance.WriteLine("#  https://www.buymeacoffee.com/christianhelle                    #");
            Logger.Instance.WriteLine("#                                                                 #");
            Logger.Instance.WriteLine("#  Does this tool not work or does it lack something you need?    #");
            Logger.Instance.WriteLine("#  https://github.com/christianhelle/apiclientcodegen/issues      #");
            Logger.Instance.WriteLine("###################################################################");
        }

        protected abstract ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace);
    }
}