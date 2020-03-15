using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools
{
    public abstract class BaseSingleFileCustomTool : ISingleFileCustomTool
    {
        public async Task Generate(ProgressMonitor monitor, ProjectFile file, SingleFileCustomToolResult result)
        {
            Bootstrapper.Initialize();
            
            var path = file.FilePath.ChangeExtension(".cs");
            result.GeneratedFilePath = path;

            var customToolNamespace = file.CustomToolNamespace;
            if (string.IsNullOrWhiteSpace(customToolNamespace))
                customToolNamespace = CustomToolService.GetFileNamespace(file, path);

            var generator = GetCodeGenerator(path, customToolNamespace);
            var progressReporter = new ProgressReporter(monitor);
            var contents = await Task.Run(() => generator.GenerateCode(progressReporter));
            await Task.Run(() => File.WriteAllText(path, contents));
        }

        protected abstract ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace);
    }
}