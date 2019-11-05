using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.CustomTool
{
    [TestClass]
    public class SingleFileCodeGeneratorTests
    {
        private const SupportedLanguage lang = SupportedLanguage.CSharp;

        [DataTestMethod]
        [DataRow(SupportedCodeGenerator.AutoRest)]
        [DataRow(SupportedCodeGenerator.NSwag)]
        [DataRow(SupportedCodeGenerator.Swagger)]
        [DataRow(SupportedCodeGenerator.OpenApi)]
        public void Generate_Test(SupportedCodeGenerator generator)
        {
            var code = Test.CreateAnnonymous<string>();
            var input = Test.CreateAnnonymous<string>();
            var contents = Test.CreateAnnonymous<string>();
            var @namespace = Test.CreateAnnonymous<string>();
            var rgbOutputFileContents = new[] { IntPtr.Zero };

            var progressMock = new Mock<IVsGeneratorProgress>();

            var generatorMock = new Mock<ICodeGenerator>();
            generatorMock
                .Setup(c => c.GenerateCode(It.IsAny<IProgressReporter>()))
                .Returns(code);

            var factoryMock = new Mock<ICodeGeneratorFactory>();
            factoryMock
                .Setup(c => c.Create(@namespace, contents, input, lang, generator))
                .Returns(generatorMock.Object);

            var sut = new TestSingleFileCodeGenerator(generator)
            {
                Factory = factoryMock.Object
            };

            var result = sut.Generate(
                input,
                contents,
                @namespace,
                rgbOutputFileContents,
                out var pcbOutput,
                progressMock.Object);

            result.Should().Be(0);
            pcbOutput.Should().Be((uint)code.Length);
            rgbOutputFileContents[0].Should().NotBe(IntPtr.Zero);

            progressMock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.Exactly(2));
        }
    }
}
