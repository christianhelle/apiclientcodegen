using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Options.NSwag;

namespace ApiClientCodeGen.VSMac.CustomTools.NSwag
{
    public interface INSwagCodeGeneratorFactory
    {
        ICodeGenerator Create(string swaggerFile,
            string defaultNamespace,
            INSwagOptions options,
            IOpenApiDocumentFactory documentFactory);
    }
}