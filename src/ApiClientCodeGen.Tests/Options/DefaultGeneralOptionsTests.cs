using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class DefaultGeneralOptionsTests
    {
        private readonly IGeneralOptions sut = new DefaultGeneralOptions();

        [TestMethod, Xunit.Fact]
        public void JavaPath_NotNull()
            => sut.JavaPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void NpmPath_NotNull()
            => sut.NpmPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void NSwagPath_NotNull()
            => sut.NSwagPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void SwaggerCodegenPath_NotNull()
            => sut.SwaggerCodegenPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void OpenApiGeneratorPath_NotNull()
            => sut.OpenApiGeneratorPath.Should().NotBeNullOrWhiteSpace();
    }
}