using System;
using System.IO;
using Rapicgen.Core;
using Rapicgen.Generators;
using Rapicgen.Options;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;

using Moq;
using Rapicgen.Core.Options.NSwag;

namespace Rapicgen.IntegrationTests.CustomTool
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    // [DeploymentItem("Resources/Swagger.json")]
    public class VisualBasicSingleFileCodeGeneratorTests
    {
        [Xunit.Fact]
        public void AutoRest_VisualBasic_Test() => Assert(SupportedCodeGenerator.AutoRest);

        [Xunit.Fact]
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
                .Setup(c => c.Create<INSwagOptions, NSwagOptionsPage, DefaultNSwagOptions>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.NSwag, optionsFactory.Object);
        }

        [Xunit.Fact]
        public void Swagger_VisualBasic_Test()
        {
            var optionsMock = new Mock<IGeneralOptions>();
            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage, DefaultGeneralOptions>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.Swagger, optionsFactory.Object);
        }

        [Xunit.Fact]
        public void OpenApi_VisualBasic_Test()
        {
            var optionsMock = new Mock<IGeneralOptions>();
            var optionsFactory = new Mock<IOptionsFactory>();
            optionsFactory
                .Setup(c => c.Create<IGeneralOptions, GeneralOptionPage, DefaultGeneralOptions>())
                .Returns(optionsMock.Object);

            Assert(SupportedCodeGenerator.OpenApi, optionsFactory.Object);
        }

        private static void Assert(
            SupportedCodeGenerator generator,
            IOptionsFactory optionsFactory = null)
        {
            var rgbOutputFileContents = new[] { IntPtr.Zero };
            var progressMock = new Mock<IProgressReporter>();

            var sut = new VisualBasicSingleFileCodeGenerator(generator);
            sut.Factory = new CodeGeneratorFactory(optionsFactory);

            var result = sut.Generate(
                Path.GetFullPath(SwaggerJsonFilename),
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
