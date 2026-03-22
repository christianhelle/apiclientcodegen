using System;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.AutoRest;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.AutoRest;

namespace Rapicgen.CLI.Commands.CSharp
{
    [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
    public interface IAutoRestCodeGeneratorFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller);
    }

    [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
    public class AutoRestCodeGeneratorFactory : IAutoRestCodeGeneratorFactory
    {
#pragma warning disable CS0618 // Type or member is obsolete
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller)
            => new AutoRestCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                options,
                processLauncher,
                documentFactory,
                dependencyInstaller);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}