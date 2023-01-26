using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.CLI.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;
using FluentAssertions;
using Rapicgen.CLI.Commands.CSharp;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class OpenApiCSharpGeneratorFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            OpenApiCSharpGeneratorFactory sut,
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