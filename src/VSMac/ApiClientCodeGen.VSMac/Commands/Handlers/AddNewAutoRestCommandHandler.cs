using System.Threading.Tasks;
using ApiClientCodeGen.VSMac.CustomTools.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewAutoRestCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => AutoRestSingleFileCustomTool.GeneratorName;

        protected override SupportedCodeGenerator CodeGeneratorType
            => SupportedCodeGenerator.AutoRest;
    }
}
