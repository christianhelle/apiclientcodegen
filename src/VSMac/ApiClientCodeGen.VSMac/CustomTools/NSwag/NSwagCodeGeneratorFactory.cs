using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Options.NSwag;

namespace ApiClientCodeGen.VSMac.CustomTools.NSwag
{
    public class NSwagCodeGeneratorFactory : INSwagCodeGeneratorFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            INSwagOptions options,
            IOpenApiDocumentFactory documentFactory)
            => new NSwagCSharpCodeGenerator(
                swaggerFile,
                documentFactory,
                new NSwagCodeGeneratorSettingsFactory(defaultNamespace, options));
    }
}