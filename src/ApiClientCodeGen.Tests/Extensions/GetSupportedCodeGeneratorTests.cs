using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    [TestClass]
    public class GetSupportedCodeGeneratorTests
    {
        [TestMethod]
        public void GetSupportedCodeGenerator_AutoRest()
            => typeof(AutoRestCodeGenerator)
                .GetSupportedCodeGenerator()
                .Should()
                .Be(SupportedCodeGenerator.AutoRest);

        [TestMethod]
        public void GetSupportedCodeGenerator_NSwag()
            => typeof(NSwagCodeGenerator)
                .GetSupportedCodeGenerator()
                .Should()
                .Be(SupportedCodeGenerator.NSwag);

        [TestMethod]
        public void GetSupportedCodeGenerator_Swagger()
            => typeof(SwaggerCodeGenerator)
                .GetSupportedCodeGenerator()
                .Should()
                .Be(SupportedCodeGenerator.Swagger);

        [TestMethod]
        public void GetSupportedCodeGenerator_OpenApi()
            => typeof(OpenApiCodeGenerator)
                .GetSupportedCodeGenerator()
                .Should()
                .Be(SupportedCodeGenerator.OpenApi);

        [TestMethod]
        public void Throws_NotSupported()
            => new Action(() => GetType().GetSupportedCodeGenerator())
                .Should()
                .ThrowExactly<NotSupportedException>();
    }
}