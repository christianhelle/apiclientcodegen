using Rapicgen.Core.Extensions;
using Rapicgen.Core.Options.OpenApiGenerator;
using FluentAssertions;
using System;
using System.ComponentModel;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Extensions
{
    public class EnumGetDescriptionTests
    {
        [Theory]
        [InlineData(OpenApiSupportedTargetFramework.NetStandard13, "netstandard1.3")]
        [InlineData(OpenApiSupportedTargetFramework.NetStandard14, "netstandard1.4")]
        [InlineData(OpenApiSupportedTargetFramework.NetStandard15, "netstandard1.5")]
        [InlineData(OpenApiSupportedTargetFramework.NetStandard16, "netstandard1.6")]
        [InlineData(OpenApiSupportedTargetFramework.NetStandard20, "netstandard2.0")]
        [InlineData(OpenApiSupportedTargetFramework.NetStandard21, "netstandard2.1")]
        [InlineData(OpenApiSupportedTargetFramework.Net47, "net47")]
        [InlineData(OpenApiSupportedTargetFramework.Net48, "net48")]
        [InlineData(OpenApiSupportedTargetFramework.Net60, "net6.0")]
        [InlineData(OpenApiSupportedTargetFramework.Net70, "net7.0")]
        [InlineData(OpenApiSupportedTargetFramework.Net80, "net8.0")]
        public void GetDescription_Returns_Description(
            OpenApiSupportedTargetFramework framework,
            string expected)
            => framework
                .GetDescription()
                .Should()
                .Be(expected);

        [Fact]
        public void GetDescription_Throws_Exception()
            => new Action(() => PlatformID.Unix.GetDescription())
                .Should()
                .Throw<InvalidEnumArgumentException>();
    }
}
