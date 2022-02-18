using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Tests.Extensions
{
    public class SupportedCodeGeneratorNameTests
    {
        [Fact]
        public void GetName_AutoRest()
            => SupportedCodeGenerator.AutoRest
                .GetName()
                .Should()
                .Be(SupportedCodeGenerator.AutoRest.ToString());

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
}