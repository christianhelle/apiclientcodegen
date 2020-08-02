using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ApiClientCodeGen.VSMac.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools
{
    public abstract class BaseSingleFileCustomTool : ISingleFileCustomTool
    {
        protected virtual bool SupportsYaml { get; } = true;

        public async Task Generate(
            ProgressMonitor monitor,
            ProjectFile file,
            SingleFileCustomToolResult result)
        {
            Bootstrapper.Initialize();

            var swaggerFile = file.FilePath;
            var outputFile = swaggerFile.ChangeExtension(".cs");
            result.GeneratedFilePath = outputFile;

            if (!SupportsYaml && swaggerFile.FileName.EndsWithAny("yaml", "yml"))
            {
                await Task.Run(() => File.WriteAllText(outputFile, string.Empty));
                var project = IdeApp.ProjectOperations.CurrentSelectedProject;
                var item = project.Files.GetFile(file.FilePath);
                item.Generator = null;
                IdeApp.ProjectOperations.MarkFileDirty(item.FilePath);
                project.Files.Remove(outputFile);

                const string message = "Specified code generator doesn't support YAML files";
                MessageService.ShowWarning(message, "Not Supported");
                Trace.WriteLine(message);
                return;
            }

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
        }

        protected abstract ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace);
    }
}