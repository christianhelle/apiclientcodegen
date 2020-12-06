using ApiClientCodeGen.CLI.Commands;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests.Command
{
    public class SwaggerCodegenFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            SwaggerCodegenFactory sut,
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher)
            => sut.Create(
                    swaggerFile,
                    defaultNamespace,
                    options,
                    processLauncher)
                .Should()
                .NotBeNull();
    }
}