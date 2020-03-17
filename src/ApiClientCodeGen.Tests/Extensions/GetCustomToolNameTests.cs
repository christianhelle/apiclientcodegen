using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    [TestClass]
    public class GetCustomToolNameTests
    {
        [TestMethod, Xunit.Fact]
        public void GetCustomToolName_AutoRest()
            => SupportedCodeGenerator.AutoRest
                .GetCustomToolName()
                .Should()
                .Contain("AutoRest");

        [TestMethod, Xunit.Fact]
        public void GetCustomToolName_NSwag()
            => SupportedCodeGenerator.NSwag
                .GetCustomToolName()
                .Should()
                .Contain("NSwag");

        [TestMethod, Xunit.Fact]
        public void GetCustomToolName_Swagger()
            => SupportedCodeGenerator.Swagger
                .GetCustomToolName()
                .Should()
                .Contain("Swagger");

        [TestMethod, Xunit.Fact]
        public void GetCustomToolName_OpenApi()
            => SupportedCodeGenerator.OpenApi
                .GetCustomToolName()
                .Should()
                .Contain("OpenApi");
    }
}
