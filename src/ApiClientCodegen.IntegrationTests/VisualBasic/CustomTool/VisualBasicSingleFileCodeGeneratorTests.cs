using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.VisualBasic.CustomTool
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.json")]
    public class VisualBasicSingleFileCodeGeneratorTests
    {
        private const SupportedLanguage Language = SupportedLanguage.VisualBasic;

        [TestMethod]
        public void AutoRest_VisualBasic_Test() => Assert(SupportedCodeGenerator.AutoRest);
        
        [TestMethod]
        public void NSwag_VisualBasic_Test() => Assert(SupportedCodeGenerator.AutoRest);
        
        [TestMethod]
        public void Swagger_VisualBasic_Test() => Assert(SupportedCodeGenerator.AutoRest);
        
        [TestMethod]
        public void OpenApi_VisualBasic_Test() => Assert(SupportedCodeGenerator.AutoRest);

        private static void Assert(SupportedCodeGenerator generator)
        {
            var rgbOutputFileContents = new[] {IntPtr.Zero};
            var progressMock = new Mock<IVsGeneratorProgress>();

            var sut = new VisualBasicSingleFileCodeGenerator(generator);

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
