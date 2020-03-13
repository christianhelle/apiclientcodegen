using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;

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