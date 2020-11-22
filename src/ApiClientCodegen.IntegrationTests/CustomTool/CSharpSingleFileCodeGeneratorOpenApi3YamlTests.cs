using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.CustomTool
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class CSharpSingleFileCodeGeneratorOpenApi3YamlTests : TestWithResources
    {
        // TODO: Re-enable once AutoRest works properly for C#
        //[Fact]
        //public void AutoRest_CSharp_Test()
        //{
        //    var optionsMock = new Mock<IAutoRestOptions>();
        //    var optionsFactory = new Mock<IOptionsFactory>();
        //    optionsFactory
        //        .Setup(c => c.Create<IAutoRestOptions, AutoRestOptionsPage>())
        //        .Returns(optionsMock.Object);

        //    Assert(SupportedCodeGenerator.AutoRest, optionsFactory.Object);
        //}

        [Fact]
        public void Swagger_CSharp_Test()
        {
            var optionsMock = new Mock<IGeneralOptions>();
            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.Swagger, optionsFactory.Object);
        }

        [Fact]
        public void OpenApi_CSharp_Test()
        {
            var optionsMock = new Mock<IGeneralOptions>();
            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.OpenApi, optionsFactory.Object);
        }

        private void Assert(
            SupportedCodeGenerator generator,
            IOptionsFactory optionsFactory = null)
        {
            var rgbOutputFileContents = new[] { IntPtr.Zero };
            var progressMock = new Mock<IVsGeneratorProgress>();

            var sut = new CSharpSingleFileCodeGenerator(generator);
            sut.Factory = new CodeGeneratorFactory(optionsFactory);

            var result = sut.Generate(
                Path.GetFullPath(SwaggerV3YamlFilename),
                string.Empty,
                typeof(CSharpSingleFileCodeGeneratorTests).Namespace,
                rgbOutputFileContents,
                out var pcbOutput,
                progressMock.Object);

            result.Should().Be(0);
            pcbOutput.Should().NotBe(0);
            rgbOutputFileContents[0].Should().NotBe(IntPtr.Zero);
        }
    }
}
