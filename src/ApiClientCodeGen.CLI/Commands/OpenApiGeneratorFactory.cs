using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;

namespace ApiClientCodeGen.CLI.Commands
{
    public interface IOpenApiGeneratorFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher);
    }

    public class OpenApiGeneratorFactory : IOpenApiGeneratorFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher)
            => new OpenApiCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                options,
                processLauncher);
    }
}