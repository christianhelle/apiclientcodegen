using System;
using System.IO;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core;
using Rapicgen.Core.Exceptions;
using Rapicgen.Core.Options;
using Rapicgen.Core.Options.AutoRest;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;
using Rapicgen.Generators;
using Rapicgen.Options.AutoRest;
using Rapicgen.Options.General;
using Rapicgen.Options.OpenApiGenerator;
using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.VisualStudio.Shell.Interop;

namespace Rapicgen.IntegrationTests.CustomTool
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class CSharpSingleFileCodeGeneratorYamlTests : TestWithResources
    {
        [SkippableFact(typeof(CustomToolException))]
        public void AutoRest_CSharp_Test()
        {
            var optionsMock = new Mock<IAutoRestOptions>();
            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<IAutoRestOptions, AutoRestOptionsPage, DefaultAutoRestOptions>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.AutoRest, optionsFactory.Object);
        }

        [Fact]
        public void Swagger_CSharp_Test()
        {
            var optionsMock = new Mock<IGeneralOptions>();
            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage, DefaultGeneralOptions>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.Swagger, optionsFactory.Object);
        }

        [Fact]
        public void OpenApi_CSharp_Test()
        {
            var optionsMock = new Mock<IGeneralOptions>();
            var openApiOptionsMock = new Mock<IOpenApiGeneratorOptions>();
            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage, DefaultGeneralOptions>())
                .Returns(optionsMock.Object);

            optionsFactory
                .Setup(c => c.Create<IOpenApiGeneratorOptions, OpenApiGeneratorOptionsPage, DefaultOpenApiGeneratorOptions>())
                .Returns(openApiOptionsMock.Object);

            Assert(SupportedCodeGenerator.OpenApi, optionsFactory.Object);
        }

        private void Assert(
            SupportedCodeGenerator generator,
            IOptionsFactory optionsFactory = null)
        {
            var rgbOutputFileContents = new[] { IntPtr.Zero };
            var progressMock = new Mock<IVsGeneratorProgress>();

            var sut = new CSharpSingleFileCodeGenerator(generator);
            sut.Factory = new CodeGeneratorFactory(optionsFactory, null);

            var result = sut.Generate(
                Path.GetFullPath(SwaggerYamlFilename),
                string.Empty,
                "GeneratedCode",
                rgbOutputFileContents,
                out var pcbOutput,
                progressMock.Object);

            result.Should().Be(0);
            pcbOutput.Should().NotBe(0);
            rgbOutputFileContents[0].Should().NotBe(IntPtr.Zero);
        }
    }
}
