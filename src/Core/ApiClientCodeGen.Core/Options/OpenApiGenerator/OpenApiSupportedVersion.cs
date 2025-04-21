using System.ComponentModel;

namespace Rapicgen.Core.Options.OpenApiGenerator;

public enum OpenApiSupportedVersion
{
    [Description("7.12.0")]
    V7120,
    [Description("7.11.0")]
    V7110,
    [Description("7.10.0")]
    V7100,
}
