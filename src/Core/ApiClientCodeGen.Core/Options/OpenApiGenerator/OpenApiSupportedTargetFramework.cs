using System.ComponentModel;

namespace Rapicgen.Core.Options.OpenApiGenerator
{
    public enum OpenApiSupportedTargetFramework
    {
        [Description("netstandard2.1")]
        NetStandard21,
        [Description("netstandard2.0")]
        NetStandard20,
        [Description("netstandard1.6")]
        NetStandard16,
        [Description("netstandard1.5")]
        NetStandard15,
        [Description("netstandard1.4")]
        NetStandard14,
        [Description("netstandard1.3")]
        NetStandard13,
        [Description("net47")]
        Net47,
        [Description("net48")]
        Net48,
        [Description("net6.0")]
        Net60,
        [Description("net7.0")]
        Net70,
        [Description("net8.0")]
        Net80,
        [Description("net9.0")]
        Net90,
    }
}
