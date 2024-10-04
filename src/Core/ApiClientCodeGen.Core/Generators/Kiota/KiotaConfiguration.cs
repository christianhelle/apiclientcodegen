using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rapicgen.Core.Generators.Kiota;

public class KiotaConfiguration
{
    [JsonPropertyName("descriptionHash")]
    public string? DescriptionHash { get; set; }

    [JsonPropertyName("descriptionLocation")]
    public string? DescriptionLocation { get; set; }

    [JsonPropertyName("lockFileVersion")]
    public string? LockFileVersion { get; set; }

    [JsonPropertyName("kiotaVersion")]
    public string? KiotaVersion { get; set; }

    [JsonPropertyName("clientClassName")]
    public string? ClientClassName { get; set; }
        
    [JsonPropertyName("typeAccessModifier")]
    public TypeAccessModifier? TypeAccessModifier { get; set; }

    [JsonPropertyName("clientNamespaceName")]
    public string? ClientNamespaceName { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("usesBackingStore")]
    public bool UsesBackingStore { get; set; }

    [JsonPropertyName("includeAdditionalData")]
    public bool IncludeAdditionalData { get; set; }

    [JsonPropertyName("serializers")]
    public List<string>? Serializers { get; set; }

    [JsonPropertyName("deserializers")]
    public List<string>? Deserializers { get; set; }

    [JsonPropertyName("structuredMimeTypes")]
    public List<string>? StructuredMimeTypes { get; set; }

    [JsonPropertyName("includePatterns")]
    public List<string>? IncludePatterns { get; set; }

    [JsonPropertyName("excludePatterns")]
    public List<string>? ExcludePatterns { get; set; }

    [JsonPropertyName("disabledValidationRules")]
    public List<object>? DisabledValidationRules { get; set; }
}