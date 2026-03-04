using FluentAssertions;
using Rapicgen.Core;
using Rapicgen.Core.Extensions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Extensions;

public class CodeGeneratorNameExtensionsTests
{
    [Fact]
    public void GetName_Swagger_ReturnsSwaggerCodegenCli()
        => SupportedCodeGenerator.Swagger
            .GetName()
            .Should()
            .Be("Swagger Codegen CLI");

    [Fact]
    public void GetName_OpenApi_ReturnsOpenApiGenerator()
        => SupportedCodeGenerator.OpenApi
            .GetName()
            .Should()
            .Be("OpenAPI Generator");

    [Fact]
    public void GetName_NSwagStudio_ReturnsNSwagStudio()
        => SupportedCodeGenerator.NSwagStudio
            .GetName()
            .Should()
            .Be("NSwag Studio");

    [Fact]
    public void GetName_Kiota_ReturnsMicrosoftKiota()
        => SupportedCodeGenerator.Kiota
            .GetName()
            .Should()
            .Be("Microsoft Kiota");

    [Fact]
    public void GetName_NSwag_ReturnsNSwag()
        => SupportedCodeGenerator.NSwag
            .GetName()
            .Should()
            .Be("NSwag");

    [Fact]
    public void GetName_AutoRest_ReturnsAutoRest()
        => SupportedCodeGenerator.AutoRest
            .GetName()
            .Should()
            .Be("AutoRest");

    [Fact]
    public void GetName_AutoRestV3_ReturnsAutoRestV3()
        => SupportedCodeGenerator.AutoRestV3
            .GetName()
            .Should()
            .Be("AutoRestV3");

    [Fact]
    public void GetName_Refitter_ReturnsRefitter()
        => SupportedCodeGenerator.Refitter
            .GetName()
            .Should()
            .Be("Refitter");
}
