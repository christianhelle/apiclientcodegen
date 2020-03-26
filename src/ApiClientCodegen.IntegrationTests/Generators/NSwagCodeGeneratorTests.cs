using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using FluentAssertions;

using Moq;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagCodeGeneratorTests : TestWithResources
    {
        private static readonly Mock<IProgressReporter> mock = new Mock<IProgressReporter>();
        private static readonly Mock<INSwagOptions> optionsMock = new Mock<INSwagOptions>();
        private readonly string code = null;

        public NSwagCodeGeneratorTests()
        {
            optionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            optionsMock.Setup(c => c.InjectHttpClient).Returns(true);
            optionsMock.Setup(c => c.GenerateClientInterfaces).Returns(true);
            optionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            optionsMock.Setup(c => c.UseBaseUrl).Returns(true);
            optionsMock.Setup(c => c.ClassStyle).Returns(CSharpClassStyle.Poco);

            var defaultNamespace = typeof(NSwagCodeGeneratorTests).Namespace;
            var codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                new OpenApiDocumentFactory(),
                new NSwagCodeGeneratorSettingsFactory(defaultNamespace, optionsMock.Object));

            code = codeGenerator.GenerateCode(mock.Object);
        }

        [Xunit.Fact]
        public void NSwag_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void NSwag_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_InjectHttpClient_From_Options()
            => optionsMock.Verify(c => c.InjectHttpClient);

        [Xunit.Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => optionsMock.Verify(c => c.GenerateClientInterfaces);

        [Xunit.Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => optionsMock.Verify(c => c.GenerateDtoTypes);

        [Xunit.Fact]
        public void Reads_UseBaseUrl_From_Options()
            => optionsMock.Verify(c => c.UseBaseUrl);

        [Xunit.Fact]
        public void Reads_ClassStyle_From_Options()
            => optionsMock.Verify(c => c.ClassStyle);

        [Xunit.Fact]
        public void Reads_UseDocumentTitle_From_Options()
            => optionsMock.Verify(c => c.UseDocumentTitle);

        [Xunit.Fact]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(
                ProjectTypes.DotNetCoreApp,
                code,
                SupportedCodeGenerator.NSwag);

        [Xunit.Fact]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(
                ProjectTypes.DotNetStandardLibrary,
                code,
                SupportedCodeGenerator.NSwag);
    }
}
