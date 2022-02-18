using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Commands;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.OpenApiGenerator;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Command
{
    public class OpenApiGeneratorFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            OpenApiGeneratorFactory sut,
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IOpenApiGeneratorOptions openApiGeneratorOptions,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => sut.Create(
                    swaggerFile,
                    defaultNamespace,
                    options,
                    openApiGeneratorOptions,
                    processLauncher,
                    dependencyInstaller)
                .Should()
                .NotBeNull();
    }
}