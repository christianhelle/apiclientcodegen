using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Refitter;

namespace ApiClientCodeGen.VSMac.CustomTools.Refitter
{
    public class RefitterSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "RefitterCodeGenerator";

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace)
            => new RefitterCodeGenerator(swaggerFile, customToolNamespace);
    }
}