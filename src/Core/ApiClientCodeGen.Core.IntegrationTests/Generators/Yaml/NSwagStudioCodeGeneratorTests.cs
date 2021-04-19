using ApiClientCodeGen.Tests.Common.Build;
using ApiClientCodeGen.Tests.Common.Fixtures.Yaml;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests.Generators.Yaml
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagStudioCodeGeneratorTests : IClassFixture<NSwagStudioCodeGeneratorFixture>
    {
        private readonly string code;

        public NSwagStudioCodeGeneratorTests(NSwagStudioCodeGeneratorFixture fixture)
        {
            code = fixture.Code;
        }

        [SkippableFact(typeof(ProcessLaunchException))]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetCoreApp,
                    code,
                    SupportedCodeGenerator.NSwagStudio)
                .Should()
                .BeTrue();

        [SkippableFact(typeof(ProcessLaunchException))]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetStandardLibrary,
                    code,
                    SupportedCodeGenerator.NSwagStudio)
                .Should()
                .BeTrue();
    }
}