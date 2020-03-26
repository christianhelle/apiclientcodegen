using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using FluentAssertions;

using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class OpenApiCodeGeneratorTests : TestWithResources
    {
        private static readonly Mock<IProgressReporter> mock = new Mock<IProgressReporter>();
        private static Mock<IGeneralOptions> optionsMock;
        private readonly string code = null;

        public OpenApiCodeGeneratorTests()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetJavaPath());

            var codeGenerator = new OpenApiCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                typeof(OpenApiCodeGeneratorTests).Namespace,
                optionsMock.Object,
                new ProcessLauncher());

            code = codeGenerator.GenerateCode(mock.Object);
        }

        // [ClassCleanup]
        public static void CleanUp()
            => DependencyUninstaller.UninstallOpenApiGenerator();

        [Xunit.Fact]
        public void OpenApi_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void OpenApi_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()), 
                Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_JavaPath_From_Options() 
            => optionsMock.Verify(c => c.JavaPath);

        [Xunit.Fact]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetCoreApp, code, SupportedCodeGenerator.OpenApi);

        [Xunit.Fact]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetStandardLibrary, code, SupportedCodeGenerator.OpenApi);

        //[Xunit.Fact]
        //public void GeneratedCode_Can_Build_In_NetFrameworkApp()
        //    => BuildHelper.BuildCSharp(ProjectTypes.DotNetFramework, code, SupportedCodeGenerator.OpenApi);
    }
}
