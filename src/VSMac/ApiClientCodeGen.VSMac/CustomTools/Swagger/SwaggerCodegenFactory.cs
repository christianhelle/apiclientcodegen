using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.NSwag;

namespace ApiClientCodeGen.VSMac.CustomTools.Swagger
{
    public interface ISwaggerCodegenFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller);
    }

    public class SwaggerCodegenFactory : ISwaggerCodegenFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => new SwaggerCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                options,
                processLauncher,
                dependencyInstaller);
    }
}