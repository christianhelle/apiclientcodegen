using FluentAssertions;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Options.Kiota;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Options;

public class DefaultKiotaOptionsTests
{
    private readonly IKiotaOptions sut = new DefaultKiotaOptions();

    [Fact]
    public void GenerateMultipleFiles_DefaultFalse()
        => sut.GenerateMultipleFiles.Should().BeFalse();

    [Fact]
    public void TypeAccessModifier_DefaultPublic()
        => sut.TypeAccessModifier.Should().Be(TypeAccessModifier.Public);

    [Fact]
    public void UsesBackingStore_DefaultFalse()
        => sut.UsesBackingStore.Should().BeFalse();
}
