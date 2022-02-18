using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ApiClientCodeGen.VSMac.CustomTools.OpenApi;
using ApiClientCodeGen.VSMac.CustomTools.Swagger;
using ApiClientCodeGen.VSMac.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
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
            await Task.Run(() => File.WriteAllText(outputFile, contents));            
                
            Trace.WriteLine(Environment.NewLine);
            Trace.WriteLine($"Output file size: {new FileInfo(outputFile).Length}");
            
            Trace.WriteLine(Environment.NewLine);
            Trace.WriteLine("###################################################################");
            Trace.WriteLine("#  Do you find this tool useful?                                  #");
            Trace.WriteLine("#  https://www.buymeacoffee.com/christianhelle                    #");
            Trace.WriteLine("#                                                                 #");
            Trace.WriteLine("#  Does this tool not work or does it lack something you need?    #");
            Trace.WriteLine("#  https://github.com/christianhelle/apiclientcodegen/issues      #");
            Trace.WriteLine("###################################################################");
        }

        protected abstract ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace);
    }
}