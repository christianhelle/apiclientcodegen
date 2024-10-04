using System.Text.Json.Serialization;

namespace Rapicgen.Core.Generators.Kiota;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TypeAccessModifier
{
    Public,
    Internal,
    Private,
    Protected
}