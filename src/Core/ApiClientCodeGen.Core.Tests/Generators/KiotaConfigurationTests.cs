using System.Text.Json;
using FluentAssertions;
using Rapicgen.Core.Generators.Kiota;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators;

public class KiotaConfigurationTests
{
    [Fact]
    public void DefaultProperties_AreNull()
    {
        var sut = new KiotaConfiguration();

        sut.DescriptionHash.Should().BeNull();
        sut.DescriptionLocation.Should().BeNull();
        sut.LockFileVersion.Should().BeNull();
        sut.KiotaVersion.Should().BeNull();
        sut.ClientClassName.Should().BeNull();
        sut.ClientNamespaceName.Should().BeNull();
        sut.Language.Should().BeNull();
        sut.Serializers.Should().BeNull();
        sut.Deserializers.Should().BeNull();
        sut.StructuredMimeTypes.Should().BeNull();
        sut.IncludePatterns.Should().BeNull();
        sut.ExcludePatterns.Should().BeNull();
        sut.DisabledValidationRules.Should().BeNull();
    }

    [Fact]
    public void DefaultBoolProperties_AreFalse()
    {
        var sut = new KiotaConfiguration();

        sut.UsesBackingStore.Should().BeFalse();
        sut.IncludeAdditionalData.Should().BeFalse();
    }

    [Fact]
    public void CanSerializeAndDeserialize()
    {
        var config = new KiotaConfiguration
        {
            ClientClassName = "TestClient",
            ClientNamespaceName = "TestNamespace",
            Language = "CSharp",
            TypeAccessModifier = TypeAccessModifier.Public,
            UsesBackingStore = true
        };

        var json = JsonSerializer.Serialize(config);
        var deserialized = JsonSerializer.Deserialize<KiotaConfiguration>(json);

        deserialized!.ClientClassName.Should().Be("TestClient");
        deserialized.ClientNamespaceName.Should().Be("TestNamespace");
        deserialized.Language.Should().Be("CSharp");
        deserialized.TypeAccessModifier.Should().Be(TypeAccessModifier.Public);
        deserialized.UsesBackingStore.Should().BeTrue();
    }

    [Fact]
    public void JsonPropertyNames_AreCorrect()
    {
        var config = new KiotaConfiguration
        {
            ClientClassName = "Test"
        };

        var json = JsonSerializer.Serialize(config);

        json.Should().Contain("\"clientClassName\"");
        json.Should().NotContain("\"ClientClassName\"");
    }
}
