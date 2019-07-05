using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.CustomTool
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.json")]
    public class CSharpSingleFileCodeGeneratorTests
    {
        [TestMethod]
        public void AutoRest_CSharp_Test() => Assert(SupportedCodeGenerator.AutoRest);
        
        [TestMethod]
        public void NSwag_CSharp_Test() => Assert(SupportedCodeGenerator.AutoRest);
        
        [TestMethod]
        public void Swagger_CSharp_Test() => Assert(SupportedCodeGenerator.AutoRest);
        
        [TestMethod]
        public void OpenApi_CSharp_Test() => Assert(SupportedCodeGenerator.AutoRest);

        private static void Assert(SupportedCodeGenerator generator)
        {
            var rgbOutputFileContents = new[] {IntPtr.Zero};
            var progressMock = new Mock<IVsGeneratorProgress>();

            var sut = new CSharpSingleFileCodeGenerator(generator);

            var result = sut.Generate(
                Path.GetFullPath("Swagger.json"),
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
