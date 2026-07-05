using System;
using FluentAssertions;
using Rapicgen.Core;
using Rapicgen.CustomTool.NSwag;
using Rapicgen.CustomTool.OpenApi;
using Rapicgen.CustomTool.Swagger;
using Rapicgen.Extensions;

namespace Rapicgen.Tests.Extensions
{
    public class GetSupportedCodeGeneratorTests
    {
        [Xunit.Fact]
        public void GetSupportedCodeGenerator_NSwag()
            => typeof(NSwagCodeGenerator)
                .GetSupportedCodeGenerator()
                .Should()
                .Be(SupportedCodeGenerator.NSwag);

        [Xunit.Fact]
        public void GetSupportedCodeGenerator_Swagger()
            => typeof(SwaggerCodeGenerator)
                .GetSupportedCodeGenerator()
                .Should()
                .Be(SupportedCodeGenerator.Swagger);

        [Xunit.Fact]
        public void GetSupportedCodeGenerator_OpenApi()
            => typeof(OpenApiCodeGenerator)
                .GetSupportedCodeGenerator()
                .Should()
                .Be(SupportedCodeGenerator.OpenApi);

        [Xunit.Fact]
        public void Throws_NotSupported()
            => new Action(() => GetType().GetSupportedCodeGenerator())
                .Should()
                .ThrowExactly<NotSupportedException>();
    }
}
