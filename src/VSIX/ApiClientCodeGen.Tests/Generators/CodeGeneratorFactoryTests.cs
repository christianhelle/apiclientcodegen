using System;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core;
using Rapicgen.Core.Generators.AutoRest;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Options;
using Rapicgen.Core.Options.AutoRest;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.OpenApiGenerator;
using Rapicgen.Generators;
using Rapicgen.Options.AutoRest;
using Rapicgen.Options.General;
using Rapicgen.Options.NSwag;
using Rapicgen.Options.OpenApiGenerator;
using FluentAssertions;
using Moq;

namespace Rapicgen.Tests.Generators
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
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage, DefaultGeneralOptions>())
                .Returns(Test.CreateDummy<IGeneralOptions>());
            mockFactory
                .Setup(c => c.Create<IAutoRestOptions, AutoRestOptionsPage>())
                .Returns(Test.CreateDummy<IAutoRestOptions>());
            mockFactory
                .Setup(c => c.Create<IOpenApiGeneratorOptions, OpenApiGeneratorOptionsPage, DefaultOpenApiGeneratorOptions>())
                .Returns(Test.CreateDummy<IOpenApiGeneratorOptions>());

            sut = new CodeGeneratorFactory(mockFactory.Object, null);
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