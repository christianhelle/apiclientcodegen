using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools.OpenApi
{
    public class OpenApiSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "OpenApiCodeGenerator";

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile, 
            string customToolNamespace)
        {
            throw new System.NotImplementedException();
        }
    }
}