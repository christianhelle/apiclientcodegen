using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using FluentAssertions;

using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class AutoRestCodeGeneratorTests : TestWithResources
    {
        private static readonly Mock<IProgressReporter> mock = new Mock<IProgressReporter>();
        private static readonly Mock<IAutoRestOptions> optionsMock = new Mock<IAutoRestOptions>();
        private readonly string code = null;

        public AutoRestCodeGeneratorTests()
        {
            optionsMock.Setup(c => c.AddCredentials).Returns(true);
            optionsMock.Setup(c => c.UseDateTimeOffset).Returns(true);
            optionsMock.Setup(c => c.UseInternalConstructors).Returns(true);

            var codeGenerator = new AutoRestCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                typeof(AutoRestCodeGeneratorTests).Namespace,
                optionsMock.Object,
                new ProcessLauncher());

            optionsMock.Setup(c => c.OverrideClientName).Returns(true);
            code = codeGenerator.GenerateCode(mock.Object);
        }

        // [ClassCleanup]
        public static void CleanUp()
            => DependencyUninstaller.UninstallAutoRest();

        [Xunit.Fact]
        public void AutoRest_CSharp_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void AutoRest_CSharp_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()), 
                Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_AddCredentials_From_Options() 
            => optionsMock.Verify(c => c.AddCredentials, Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_ClientSideValidation_From_Options() 
            => optionsMock.Verify(c => c.ClientSideValidation, Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_OverrideClientName_From_Options() 
            => optionsMock.Verify(c => c.OverrideClientName, Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_SyncMethods_From_Options() 
            => optionsMock.Verify(c => c.SyncMethods, Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_UseDateTimeOffset_From_Options() 
            => optionsMock.Verify(c => c.UseDateTimeOffset, Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_UseInternalConstructors_From_Options() 
            => optionsMock.Verify(c => c.UseInternalConstructors, Times.AtLeastOnce);

        [Xunit.Fact]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(
                ProjectTypes.DotNetCoreApp,
                code,
                SupportedCodeGenerator.AutoRest);

        [Xunit.Fact]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(
                ProjectTypes.DotNetStandardLibrary,
                code,
                SupportedCodeGenerator.AutoRest);
    }
}
