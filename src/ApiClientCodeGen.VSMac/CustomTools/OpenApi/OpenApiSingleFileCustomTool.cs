using System.Threading.Tasks;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools.OpenApi
{
    public class OpenApiSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "OpenApiCodeGenerator";

        protected override Task OnGenerate(
            ProgressMonitor monitor, 
            ProjectFile file, 
            SingleFileCustomToolResult result)
        {
            throw new System.NotImplementedException();
        }
    }
}