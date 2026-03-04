using FluentAssertions;
using Rapicgen.Core.Generators;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators;

public class FileValidatorTests
{
    [Theory]
    [InlineData("spec.json")]
    [InlineData("spec.yaml")]
    [InlineData("spec.yml")]
    [InlineData("spec.nswag")]
    [InlineData("spec.refitter")]
    [InlineData("C:\\path\\to\\spec.JSON")]
    [InlineData("C:\\path\\to\\spec.YML")]
    public void IsSupportedOpenApiFile_Returns_True_For_Supported_Extensions(string filePath)
        => FileValidator.IsSupportedOpenApiFile(filePath).Should().BeTrue();

    [Theory]
    [InlineData("file.cs")]
    [InlineData("file.csproj")]
    [InlineData("file.md")]
    [InlineData("file.txt")]
    [InlineData("Dockerfile")]
    [InlineData("file.dockerfile")]
    [InlineData("file.xml")]
    public void IsSupportedOpenApiFile_Returns_False_For_Unsupported_Extensions(string filePath)
        => FileValidator.IsSupportedOpenApiFile(filePath).Should().BeFalse();

    [Theory]
    [InlineData("version: '3'\nservices:\n  web:\n    image: nginx")]
    [InlineData("services:\n  db:\n    image: postgres")]
    [InlineData("# docker-compose\nversion: '3.8'")]
    public void IsNonOpenApiContent_Returns_True_For_Docker_Compose(string content)
        => FileValidator.IsNonOpenApiContent(content, "docker-compose.yml").Should().BeTrue();

    [Theory]
    [InlineData("openapi: 3.0.0\ninfo:\n  title: My API")]
    [InlineData("swagger: '2.0'\ninfo:\n  title: My API")]
    public void IsNonOpenApiContent_Returns_False_For_OpenApi_Spec(string content)
        => FileValidator.IsNonOpenApiContent(content, "spec.yaml").Should().BeFalse();

    [Fact]
    public void IsNonOpenApiContent_Returns_False_For_Json_Files()
        => FileValidator.IsNonOpenApiContent("version: '3'\nservices:", "spec.json").Should().BeFalse();

    [Fact]
    public void IsNonOpenApiContent_Returns_False_For_Null_Content()
        => FileValidator.IsNonOpenApiContent(null, "file.yml").Should().BeFalse();

    [Fact]
    public void IsNonOpenApiContent_Returns_False_For_Empty_Content()
        => FileValidator.IsNonOpenApiContent(string.Empty, "file.yml").Should().BeFalse();
}
