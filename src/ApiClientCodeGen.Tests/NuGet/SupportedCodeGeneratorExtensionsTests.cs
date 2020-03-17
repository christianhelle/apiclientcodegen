using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.NuGet
{
    [TestClass]
    public class SupportedCodeGeneratorExtensionsTests
    {
        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwag_Returns_NotEmpty()
            => SupportedCodeGenerator.NSwag
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwagStudio_Returns_NotEmpty()
            => SupportedCodeGenerator.NSwagStudio
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_AutoRest_Returns_NotEmpty()
            => SupportedCodeGenerator.AutoRest
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_Returns_NotEmpty()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi_Returns_NotEmpty()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_OpenApi_Same_Dependencies()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .BeEquivalentTo(
                    SupportedCodeGenerator.OpenApi
                            .GetDependencies());

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwag_Contains_NewtonsoftJson()
            => SupportedCodeGenerator.NSwag
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Newtonsoft.Json");

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwagStudio_Contains_NewtonsoftJson()
            => SupportedCodeGenerator.NSwagStudio
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Newtonsoft.Json");

        [TestMethod, Xunit.Fact]
        public void GetDependencies_AutoRest_Contains_RestClientRuntime()
            => SupportedCodeGenerator.AutoRest
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Microsoft.Rest.ClientRuntime");

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_Contains_RestSharp()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "RestSharp");

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_Contains_JsonSubTypes()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "JsonSubTypes");

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_RestSharp()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "RestSharp");

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_JsonSubTypes()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "JsonSubTypes");
    }
}
