using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests
{
    public class GeneralOptionsTests
    {
        [Theory, AutoData]
        public void JavaPath_NotNull(DefaultGeneralOptions sut)
            => sut.JavaPath.Should().NotBeNullOrWhiteSpace();
        
        [Theory, AutoData]
        public void NpmPath_NotNull(DefaultGeneralOptions sut)
            => sut.NpmPath.Should().NotBeNullOrWhiteSpace();
        
        [Theory, AutoData]
        public void NSwagPath_NotNull(DefaultGeneralOptions sut)
            => sut.NSwagPath.Should().NotBeNullOrWhiteSpace();
        
        [Theory, AutoData]
        public void SwaggerCodegenPath_NotNull(DefaultGeneralOptions sut)
            => sut.SwaggerCodegenPath.Should().NotBeNullOrWhiteSpace();
        
        [Theory, AutoData]
        public void OpenApiGeneratorPath_NotNull(DefaultGeneralOptions sut)
            => sut.OpenApiGeneratorPath.Should().NotBeNullOrWhiteSpace();
    }
}