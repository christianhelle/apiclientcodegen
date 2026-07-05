using System;
using System.Linq;
using FluentAssertions;
using Rapicgen.Core;
using Xunit;

namespace ApiClientCodeGen.Core.Tests;

[Trait("Category", "Unit")]
public class SupportedCodeGeneratorTests
{
    [Fact]
    public void Enum_Contains_NSwag()
        => Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.NSwag)
            .Should().BeTrue();

    [Fact]
    public void Enum_Does_Not_Contain_AutoRest()
        => Enum.GetNames(typeof(SupportedCodeGenerator))
            .Should().NotContain("AutoRest");

    [Fact]
    public void Enum_Does_Not_Contain_AutoRestV3()
        => Enum.GetNames(typeof(SupportedCodeGenerator))
            .Should().NotContain("AutoRestV3");

    [Fact]
    public void Enum_Contains_Swagger()
        => Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.Swagger)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_OpenApi()
        => Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.OpenApi)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_NSwagStudio()
        => Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.NSwagStudio)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_Kiota()
        => Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.Kiota)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_Refitter()
        => Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.Refitter)
            .Should().BeTrue();

    [Fact]
    public void Enum_HasExpectedCount()
        => Enum.GetValues(typeof(SupportedCodeGenerator))
            .Length.Should().Be(6);

    [Fact]
    public void All_Enum_Values_Are_Distinct()
    {
        var values = Enum.GetValues(typeof(SupportedCodeGenerator))
            .Cast<int>()
            .ToList();

        values.Should().OnlyHaveUniqueItems("each generator must have a unique enum value");
    }
}

public class SupportedLanguageTests
{
    [Fact]
    public void Enum_Contains_CSharp()
        => Enum.IsDefined(typeof(SupportedLanguage), SupportedLanguage.CSharp)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_VisualBasic()
        => Enum.IsDefined(typeof(SupportedLanguage), SupportedLanguage.VisualBasic)
            .Should().BeTrue();

    [Fact]
    public void Enum_HasExpectedCount()
        => Enum.GetValues(typeof(SupportedLanguage))
            .Length.Should().Be(2);
}
