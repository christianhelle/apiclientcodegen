using Rapicgen.Core;
using Rapicgen.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace Rapicgen.CLI.Tests.Extensions
{
    public class SupportedCodeGeneratorNameTests
    {
        #pragma warning disable CS0618 // Type or member is obsolete - These tests intentionally validate deprecated AutoRest during deprecation period
        [Fact]
        public void GetName_AutoRest()
            => SupportedCodeGenerator.AutoRest
                .GetName()
                .Should()
                .Be(SupportedCodeGenerator.AutoRest.ToString());
        #pragma warning restore CS0618

        [Fact]
        public void GetName_NSwag()
            => SupportedCodeGenerator.NSwag
                .GetName()
                .Should()
                .Be(SupportedCodeGenerator.NSwag.ToString());

        [Fact]
        public void GetName_NSwagStudio()
            => SupportedCodeGenerator.NSwagStudio
                .GetName()
                .Should()
                .Be("NSwag Studio");

        [Fact]
        public void GetName_Swagger()
            => SupportedCodeGenerator.Swagger
                .GetName()
                .Should()
                .Be("Swagger Codegen CLI");

        [Fact]
        public void GetName_OpenApi()
            => SupportedCodeGenerator.OpenApi
                .GetName()
                .Should()
                .Be("OpenAPI Generator");
    }
    #pragma warning restore CS0618
}