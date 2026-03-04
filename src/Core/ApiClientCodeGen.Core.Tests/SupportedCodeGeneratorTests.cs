using System;
using System.Linq;
using FluentAssertions;
using Rapicgen.Core;
using Xunit;

namespace ApiClientCodeGen.Core.Tests;

public class SupportedCodeGeneratorTests
{
    [Fact]
    public void Enum_Contains_NSwag()
        => Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.NSwag)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_AutoRest()
        => Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.AutoRest)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_AutoRestV3()
        => Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.AutoRestV3)
            .Should().BeTrue();

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
            .Length.Should().Be(8);
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
