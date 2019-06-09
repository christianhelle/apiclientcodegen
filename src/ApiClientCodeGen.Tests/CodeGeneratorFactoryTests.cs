using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    public class CodeGeneratorFactoryTests
    {
        private CodeGeneratorFactory sut;

        [TestInitialize]
        public void Init()
        {
            var mockFactory = new Mock<IOptionsFactory>();
            mockFactory
                .Setup(c => c.Create<INSwagOption, NSwagOptionsPage>())
                .Returns(Test.CreateDummy<INSwagOption>());
            mockFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage>())
                .Returns(Test.CreateDummy<IGeneralOptions>());

            sut = new CodeGeneratorFactory(mockFactory.Object);
        }

        [TestMethod]
        public void Can_Create_NSwagCodeGenerator()
            => sut.Create(
                string.Empty,
                string.Empty,
                string.Empty,
                SupportedLanguage.CSharp,
                SupportedCodeGenerator.NSwag)
            .Should()
            .BeOfType<NSwagCSharpCodeGenerator>();

        [TestMethod]
        public void Can_Create_AutoRestCodeGenerator()
            => sut.Create(
                string.Empty,
                string.Empty,
                string.Empty,
                SupportedLanguage.CSharp,
                SupportedCodeGenerator.AutoRest)
            .Should()
            .BeOfType<AutoRestCSharpCodeGenerator>();

        [TestMethod]
        public void Can_Create_SwaggerCodeGenerator()
            => sut.Create(
                string.Empty,
                string.Empty,
                string.Empty,
                SupportedLanguage.CSharp,
                SupportedCodeGenerator.Swagger)
            .Should()
            .BeOfType<SwaggerCSharpCodeGenerator>();

        [TestMethod]
        public void Can_Create_OpenApiCodeGenerator()
            => sut.Create(
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    SupportedLanguage.CSharp,
                    SupportedCodeGenerator.OpenApi)
                .Should()
                .BeOfType<OpenApiCSharpCodeGenerator>();
    }
}