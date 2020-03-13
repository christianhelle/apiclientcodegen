using System.Threading.Tasks;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools
{
    public abstract class BaseSingleFileCustomTool : ISingleFileCustomTool
    {
        public Task Generate(ProgressMonitor monitor, ProjectFile file, SingleFileCustomToolResult result)
        {
            Bootstrapper.Initialize();
            return OnGenerate(monitor, file, result);
        }

        protected abstract Task OnGenerate(
            ProgressMonitor monitor,
            ProjectFile file,
            SingleFileCustomToolResult result);
    }
}