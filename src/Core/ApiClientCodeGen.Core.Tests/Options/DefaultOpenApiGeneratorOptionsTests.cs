using FluentAssertions;
using Rapicgen.Core.Options.OpenApiGenerator;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Options;

public class DefaultOpenApiGeneratorOptionsTests
{
    private readonly IOpenApiGeneratorOptions sut = new DefaultOpenApiGeneratorOptions();

    [Fact]
    public void EmitDefaultValue_DefaultTrue()
        => sut.EmitDefaultValue.Should().BeTrue();

    [Fact]
    public void MethodArgument_DefaultTrue()
        => sut.MethodArgument.Should().BeTrue();

    [Fact]
    public void GeneratePropertyChanged_DefaultFalse()
        => sut.GeneratePropertyChanged.Should().BeFalse();

    [Fact]
    public void UseCollection_DefaultFalse()
        => sut.UseCollection.Should().BeFalse();

    [Fact]
    public void UseDateTimeOffset_DefaultFalse()
        => sut.UseDateTimeOffset.Should().BeFalse();

    [Fact]
    public void CustomAdditionalProperties_DefaultNull()
        => sut.CustomAdditionalProperties.Should().BeNull();

    [Fact]
    public void SkipFormModel_DefaultFalse()
        => sut.SkipFormModel.Should().BeFalse();

    [Fact]
    public void TemplatesPath_DefaultNull()
        => sut.TemplatesPath.Should().BeNull();

    [Fact]
    public void UseConfigurationFile_DefaultFalse()
        => sut.UseConfigurationFile.Should().BeFalse();

    [Fact]
    public void GenerateMultipleFiles_DefaultFalse()
        => sut.GenerateMultipleFiles.Should().BeFalse();

    [Fact]
    public void HttpUserAgent_DefaultNull()
        => sut.HttpUserAgent.Should().BeNull();
}
