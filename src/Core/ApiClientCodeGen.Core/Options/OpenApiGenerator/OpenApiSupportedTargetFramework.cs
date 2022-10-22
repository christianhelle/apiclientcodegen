﻿using System.ComponentModel;

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
        [Description("netcoreapp3.1")]
        NetCoreApp31,
        [Description("net47")]
        Net47,
        [Description("net5.0")]
        Net50,
        [Description("net6.0")]
        Net60,
    }
}