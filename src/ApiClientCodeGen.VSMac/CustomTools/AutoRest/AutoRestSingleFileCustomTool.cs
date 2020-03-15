using System.Threading.Tasks;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools.AutoRest
{
    public class AutoRestSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "AutoRestCodeGenerator";

        protected override Task OnGenerate(
            ProgressMonitor monitor, 
            ProjectFile file, 
            SingleFileCustomToolResult result)
        {
            throw new System.NotImplementedException();
        }
    }
}