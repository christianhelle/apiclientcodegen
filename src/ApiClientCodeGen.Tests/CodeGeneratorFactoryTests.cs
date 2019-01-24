using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    public class CodeGeneratorFactoryTests
    {
        private readonly CodeGeneratorFactory sut = new CodeGeneratorFactory();

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
    }
}