using FluentAssertions;
using Rapicgen.Core;
using Rapicgen.Core.NuGet;

namespace Rapicgen.Tests.NuGet
{
    public class PackageDependencyListProviderTests
    {
        private readonly PackageDependencyListProvider sut = new PackageDependencyListProvider();

        [Xunit.Fact]
        public void GetDependencies_NSwag_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_NSwagStudio_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_Swagger_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_NSwag_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [Xunit.Fact]
        public void GetDependencies_Swagger_Contains_RestSharp()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .Contain(PackageDependencies.RestSharp);

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_MicrosoftExtensionsHttp()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.MicrosoftExtensionsHttp);
    }
}
