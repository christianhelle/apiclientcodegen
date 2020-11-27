using System;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag;
using FluentAssertions;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators
{
    
    public class CodeGeneratorFactoryTests
    {
        private readonly CodeGeneratorFactory sut;

        public CodeGeneratorFactoryTests()
        {
            var mockFactory = new Mock<IOptionsFactory>();
            mockFactory
                .Setup(c => c.Create<IAutoRestOptions, AutoRestOptionsPage>())
                .Returns(Test.CreateDummy<IAutoRestOptions>());
            mockFactory
                .Setup(c => c.Create<INSwagOptions, NSwagOptionsPage>())
                .Returns(Test.CreateDummy<INSwagOptions>());
            mockFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage>())
                .Returns(Test.CreateDummy<IGeneralOptions>());
            mockFactory
                .Setup(c => c.Create<IAutoRestOptions, AutoRestOptionsPage>())
                .Returns(Test.CreateDummy<IAutoRestOptions>());

            sut = new CodeGeneratorFactory(mockFactory.Object);
        }

        [Xunit.Fact]
        public void Can_Create_NSwagCodeGenerator()
            => sut.Create(
                string.Empty,
                string.Empty,
                string.Empty,
                SupportedLanguage.CSharp,
                SupportedCodeGenerator.NSwag)
            .Should()
            .BeOfType<NSwagCSharpCodeGenerator>();

        [Xunit.Fact]
        public void Can_Create_AutoRestCodeGenerator()
            => sut.Create(
                string.Empty,
                string.Empty,
                string.Empty,
                SupportedLanguage.CSharp,
                SupportedCodeGenerator.AutoRest)
            .Should()
            .BeOfType<AutoRestCSharpCodeGenerator>();

        [Xunit.Fact]
        public void Can_Create_SwaggerCodeGenerator()
            => sut.Create(
                string.Empty,
                string.Empty,
                string.Empty,
                SupportedLanguage.CSharp,
                SupportedCodeGenerator.Swagger)
            .Should()
            .BeOfType<SwaggerCSharpCodeGenerator>();

        [Xunit.Fact]
        public void Can_Create_OpenApiCodeGenerator()
            => sut.Create(
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    SupportedLanguage.CSharp,
                    SupportedCodeGenerator.OpenApi)
                .Should()
                .BeOfType<OpenApiCSharpCodeGenerator>();

        [Xunit.Fact]
        public void Create_NSwagStudio_Throws_NotSupported()
            => new Action(
                    () => sut.Create(
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        SupportedLanguage.CSharp,
                        SupportedCodeGenerator.NSwagStudio))
                .Should()
                .ThrowExactly<NotSupportedException>();
    }
}