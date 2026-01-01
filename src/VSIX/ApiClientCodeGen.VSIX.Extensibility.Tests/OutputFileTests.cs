using System;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.VSIX.Extensibility.Tests;

public class OutputFileTests
{
    [Theory]
    [InlineData("swagger.json", "Swagger.cs")]
    [InlineData("api.yaml", "Api.cs")]
    [InlineData("openapi.yml", "Openapi.cs")]
    [InlineData("petstore.json", "Petstore.cs")]
    public void GetOutputFilename_Returns_CorrectFilename(
        string inputFile,
        string expectedFileName)
    {
        var result = OutputFile.GetOutputFilename(inputFile);
        
        result.Should().EndWith(expectedFileName);
        result.Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("C:\\path\\to\\swagger.json", "Swagger.cs")]
    [InlineData("C:\\another\\path\\api.yaml", "Api.cs")]
    public void GetOutputFilename_WithFullPath_Returns_CorrectPath(
        string inputFile,
        string expectedFileName)
    {
        var result = OutputFile.GetOutputFilename(inputFile);
        
        result.Should().EndWith(expectedFileName);
        System.IO.Path.GetDirectoryName(result).Should().Be(System.IO.Path.GetDirectoryName(inputFile));
    }

    [Fact]
    public void GetOutputFilename_CapitalizesFirstCharacter()
    {
        var inputFile = "test.json";
        
        var result = OutputFile.GetOutputFilename(inputFile);
        
        result.Should().EndWith("Test.cs");
    }

    [Fact]
    public void GetOutputFilename_ChangesExtension_ToCSharp()
    {
        var inputFile = "swagger.yaml";
        
        var result = OutputFile.GetOutputFilename(inputFile);
        
        result.Should().EndWith(".cs");
        result.Should().NotEndWith(".yaml");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void GetOutputFilename_WithEmptyInput_Throws(string inputFile)
    {
        var act = () => OutputFile.GetOutputFilename(inputFile);
        
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetOutputFilename_PreservesDirectory()
    {
        var inputFile = System.IO.Path.Combine("C:", "projects", "api", "swagger.json");
        
        var result = OutputFile.GetOutputFilename(inputFile);
        
        var expectedDir = System.IO.Path.Combine("C:", "projects", "api");
        result.Should().StartWith(expectedDir);
    }
}
