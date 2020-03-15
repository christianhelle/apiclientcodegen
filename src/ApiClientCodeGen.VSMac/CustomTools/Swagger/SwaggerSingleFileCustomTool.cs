using System.Threading.Tasks;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools.Swagger
{
    public class SwaggerSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "SwaggerCodeGenerator";

        protected override Task OnGenerate(
            ProgressMonitor monitor, 
            ProjectFile file, 
            SingleFileCustomToolResult result)
        {
            throw new System.NotImplementedException();
        }
    }
}