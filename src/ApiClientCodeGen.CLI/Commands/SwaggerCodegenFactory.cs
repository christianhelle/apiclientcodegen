using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ApiClientCodeGen.CLI.Commands
{
    public interface ISwaggerCodegenFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher);
    }

    public class SwaggerCodegenFactory : ISwaggerCodegenFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher)
            => new SwaggerCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                options,
                processLauncher);
    }
}