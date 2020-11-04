using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using NJsonSchema.CodeGeneration.CSharp;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.CustomTool
{
    
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class VisualBasicSingleFileCodeGeneratorTests
    {
        //[Fact]
        //public void AutoRest_VisualBasic_Test()
        //    => Assert(SupportedCodeGenerator.AutoRest);

        [Fact]
        public void NSwag_VisualBasic_Test()
        {
            var optionsMock = new Mock<INSwagOptions>();
            optionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            optionsMock.Setup(c => c.InjectHttpClient).Returns(true);
            optionsMock.Setup(c => c.GenerateClientInterfaces).Returns(true);
            optionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            optionsMock.Setup(c => c.UseBaseUrl).Returns(true);
            optionsMock.Setup(c => c.ClassStyle).Returns(CSharpClassStyle.Poco);

            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<INSwagOptions, NSwagOptionsPage>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.NSwag, optionsFactory.Object);
        }

        [Fact]
        public void Swagger_VisualBasic_Test()
        {
            var optionsMock = new Mock<IGeneralOptions>();
            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.Swagger, optionsFactory.Object);
        }

        [Fact]
        public void OpenApi_VisualBasic_Test()
        {
            var optionsMock = new Mock<IGeneralOptions>();
            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.OpenApi, optionsFactory.Object);
        }

        private static void Assert(
            SupportedCodeGenerator generator,
            IOptionsFactory optionsFactory = null)
        {
            var rgbOutputFileContents = new[] { IntPtr.Zero };
            var progressMock = new Mock<IVsGeneratorProgress>();

            var sut = new VisualBasicSingleFileCodeGenerator(generator);
            sut.Factory = new CodeGeneratorFactory(optionsFactory);

            var result = sut.Generate(
                Path.GetFullPath("Swagger.json"),
                string.Empty,
                typeof(VisualBasicSingleFileCodeGeneratorTests).Namespace,
                rgbOutputFileContents,
                out var pcbOutput,
                progressMock.Object);

            result.Should().Be(0);
            pcbOutput.Should().NotBe(0);
            rgbOutputFileContents[0].Should().NotBe(IntPtr.Zero);
        }
    }
}
