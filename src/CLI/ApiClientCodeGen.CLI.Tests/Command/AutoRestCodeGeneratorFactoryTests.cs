using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Commands;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Command
{
    public class AutoRestCodeGeneratorFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            AutoRestCodeGeneratorFactory sut,
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller)
            => sut.Create(
                    swaggerFile,
                    defaultNamespace,
                    options,
                    processLauncher,
                    documentFactory,
                    dependencyInstaller)
                .Should()
                .NotBeNull();
    }
}
