using System.Linq;
using FluentAssertions;
using Rapicgen.Core;
using Rapicgen.Core.NuGet;
using Rapicgen.Core.Options.OpenApiGenerator;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.NuGet;

public class PackageDependencyListProviderAdditionalTests
{
    private readonly PackageDependencyListProvider sut = new();

    [Fact]
    public void GetDependencies_Refitter_Returns_NotEmpty()
        => sut.GetDependencies(SupportedCodeGenerator.Refitter)
            .Should()
            .NotBeNullOrEmpty();

    [Fact]
    public void GetDependencies_Refitter_Contains_SystemRuntimeSerializationPrimitives()
        => sut.GetDependencies(SupportedCodeGenerator.Refitter)
            .Should()
            .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

    [Fact]
    public void GetDependencies_Refitter_Contains_SystemComponentModelAnnotations()
        => sut.GetDependencies(SupportedCodeGenerator.Refitter)
            .Should()
            .Contain(PackageDependencies.SystemComponentModelAnnotations);

    [Fact]
    public void GetDependencies_Swagger_Contains_NewtonsoftJson()
        => sut.GetDependencies(SupportedCodeGenerator.Swagger)
            .Should()
            .Contain(PackageDependencies.NewtonsoftJson);

    [Fact]
    public void GetDependencies_AutoRestV3_Returns_NotEmpty()
        => sut.GetDependencies(SupportedCodeGenerator.AutoRestV3)
            .Should()
            .NotBeNullOrEmpty();

    [Fact]
    public void GetDependencies_NSwag_DoesNotContain_RestSharp()
        => sut.GetDependencies(SupportedCodeGenerator.NSwag)
            .Should()
            .NotContain(PackageDependencies.RestSharp);

    [Fact]
    public void GetDependencies_OpenApiVersion_AtLeastV7120_ReturnsDependencies()
        => sut.GetDependencies(OpenApiSupportedVersion.V7120)
            .Should()
            .NotBeNullOrEmpty();

    [Fact]
    public void GetDependencies_OpenApiVersion_AtLeastV7120_ContainsMicrosoftExtensionsHttp()
        => sut.GetDependencies(OpenApiSupportedVersion.V7120)
            .Should()
            .Contain(PackageDependencies.MicrosoftExtensionsHttp);

    [Fact]
    public void GetDependencies_OpenApiVersion_BelowV7120_ReturnsLegacyDependencies()
        => sut.GetDependencies(OpenApiSupportedVersion.V7070)
            .Should()
            .Contain(PackageDependencies.RestSharpLatest);

    [Fact]
    public void GetDependencies_OpenApiVersion_BelowV7120_ContainsPolly()
        => sut.GetDependencies(OpenApiSupportedVersion.V7070)
            .Should()
            .Contain(PackageDependencies.Polly);

    [Fact]
    public void GetDependencies_OpenApiVersion_BelowV7120_ContainsNewtonsoft()
        => sut.GetDependencies(OpenApiSupportedVersion.V7070)
            .Should()
            .Contain(PackageDependencies.NewtonsoftJson);

    [Fact]
    public void GetDependencies_OpenApiVersion_Latest_ReturnsDependencies()
        => sut.GetDependencies(OpenApiSupportedVersion.Latest)
            .Should()
            .NotBeNullOrEmpty();

    [Theory]
    [InlineData(SupportedCodeGenerator.NSwag)]
    [InlineData(SupportedCodeGenerator.Swagger)]
    [InlineData(SupportedCodeGenerator.OpenApi)]
    [InlineData(SupportedCodeGenerator.AutoRest)]
    [InlineData(SupportedCodeGenerator.AutoRestV3)]
    [InlineData(SupportedCodeGenerator.Kiota)]
    [InlineData(SupportedCodeGenerator.Refitter)]
    [InlineData(SupportedCodeGenerator.NSwagStudio)]
    public void GetDependencies_AllGenerators_ReturnNonEmpty(SupportedCodeGenerator generator)
        => sut.GetDependencies(generator)
            .Should()
            .NotBeNullOrEmpty();

    [Theory]
    [InlineData(SupportedCodeGenerator.NSwag)]
    [InlineData(SupportedCodeGenerator.Swagger)]
    [InlineData(SupportedCodeGenerator.OpenApi)]
    [InlineData(SupportedCodeGenerator.AutoRest)]
    [InlineData(SupportedCodeGenerator.AutoRestV3)]
    [InlineData(SupportedCodeGenerator.Kiota)]
    [InlineData(SupportedCodeGenerator.Refitter)]
    [InlineData(SupportedCodeGenerator.NSwagStudio)]
    public void GetDependencies_AllGenerators_AllHaveNonEmptyNameAndVersion(SupportedCodeGenerator generator)
    {
        var dependencies = sut.GetDependencies(generator);
        dependencies.Should().AllSatisfy(d =>
        {
            d.Name.Should().NotBeNullOrWhiteSpace();
            d.Version.Should().NotBeNullOrWhiteSpace();
        });
    }
}
