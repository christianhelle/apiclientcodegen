using System;
using System.Linq;
using FluentAssertions;
using Rapicgen.Core;
using Xunit;

namespace ApiClientCodeGen.Core.Tests;

#pragma warning disable CS0618 // Type or member is obsolete - These tests intentionally validate deprecated AutoRest enum values during deprecation period
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

    [Fact]
    public void AutoRest_And_AutoRestV3_Both_Present_During_Deprecation()
    {
        // Both AutoRest enum values must remain functional during deprecation period
        // They will be removed together in Phase 3 (~Jan 2027)
        var values = Enum.GetValues(typeof(SupportedCodeGenerator))
            .Cast<SupportedCodeGenerator>()
            .ToList();

        values.Should().Contain(SupportedCodeGenerator.AutoRest,
            "AutoRest (v2) must remain available during deprecation period");
        values.Should().Contain(SupportedCodeGenerator.AutoRestV3,
            "AutoRestV3 (v3 beta) must remain available during deprecation period");
    }

    [Fact]
    public void AutoRest_Enum_Values_Have_Correct_Integer_Values()
    {
        // Validate enum integer values to prevent accidental changes during refactoring
        ((int)SupportedCodeGenerator.AutoRest).Should().Be(1, 
            "AutoRest enum value should remain stable");
        ((int)SupportedCodeGenerator.AutoRestV3).Should().Be(2,
            "AutoRestV3 enum value should remain stable");
    }

    [Fact]
    public void All_Enum_Values_Are_Distinct()
    {
        // Ensure no duplicate enum values exist
        var values = Enum.GetValues(typeof(SupportedCodeGenerator))
            .Cast<int>()
            .ToList();

        values.Should().OnlyHaveUniqueItems("each generator must have a unique enum value");
    }
}
#pragma warning restore CS0618

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
