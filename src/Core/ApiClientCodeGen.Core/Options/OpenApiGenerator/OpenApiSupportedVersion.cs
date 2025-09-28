using System.ComponentModel;
using Rapicgen.Core.TypeConverters;

namespace Rapicgen.Core.Options.OpenApiGenerator;

[TypeConverter(typeof(EnumDescriptionTypeConverter))]
public enum OpenApiSupportedVersion
{
    /// <summary>
    /// Default value that represents the latest version (maps to <see cref="V7140"/>)
    /// </summary>
    [Description("Latest")]
    Latest = 0,

    [Description("7.16.0")]
    V7160 = 7160,
    [Description("7.15.0")]
    V7150 = 7150,
    [Description("7.14.0")]
    V7140 = 7140,
    [Description("7.13.0")]
    V7130 = 7130,
    [Description("7.12.0")]
    V7120 = 7120,
    [Description("7.11.0")]
    V7110 = 7110,
    [Description("7.10.0")]
    V7100 = 7100,
    [Description("7.9.0")]
    V7090 = 7090,
    [Description("7.8.0")]
    V7080 = 7080,
    [Description("7.7.0")]
    V7070 = 7070,
}

public static class OpenApiSupportedVersionExtensions
{
    /// <summary>
    /// Gets the latest supported version of OpenAPI Generator
    /// </summary>
    public static OpenApiSupportedVersion Latest => OpenApiSupportedVersion.V7160;
}
