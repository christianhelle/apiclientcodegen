using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools.Swagger
{
    public class SwaggerSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "SwaggerCodeGenerator";

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile, 
            string customToolNamespace)
        {
            throw new System.NotImplementedException();
        }
    }
}