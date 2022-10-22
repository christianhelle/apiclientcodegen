using System;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.AutoRest;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.AutoRest;

namespace ApiClientCodeGen.VSMac.CustomTools.AutoRest
{
    public interface IAutoRestCodeGeneratorFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory factory,
            IDependencyInstaller dependencyInstaller);
    }

    public class AutoRestCodeGeneratorFactory : IAutoRestCodeGeneratorFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory factory,
            IDependencyInstaller dependencyInstaller)
            => new AutoRestCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                options,
                processLauncher,
                factory,
                dependencyInstaller);
    }
}