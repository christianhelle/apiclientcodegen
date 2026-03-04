using System;
using FluentAssertions;
using Rapicgen.Core.Generators.Kiota;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators;

public class TypeAccessModifierTests
{
    [Fact]
    public void Enum_Contains_Public()
        => Enum.IsDefined(typeof(TypeAccessModifier), TypeAccessModifier.Public)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_Internal()
        => Enum.IsDefined(typeof(TypeAccessModifier), TypeAccessModifier.Internal)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_Private()
        => Enum.IsDefined(typeof(TypeAccessModifier), TypeAccessModifier.Private)
            .Should().BeTrue();

    [Fact]
    public void Enum_Contains_Protected()
        => Enum.IsDefined(typeof(TypeAccessModifier), TypeAccessModifier.Protected)
            .Should().BeTrue();

    [Fact]
    public void Enum_HasExpectedCount()
        => Enum.GetValues(typeof(TypeAccessModifier))
            .Length.Should().Be(4);
}
