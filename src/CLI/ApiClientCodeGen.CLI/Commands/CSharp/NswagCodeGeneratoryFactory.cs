using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Options.NSwag;

namespace Rapicgen.CLI.Commands.CSharp
{
    public interface INSwagCodeGeneratorFactory
    {
        ICodeGenerator Create(string swaggerFile,
            string defaultNamespace,
            INSwagOptions options,
            IOpenApiDocumentFactory documentFactory);
    }

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