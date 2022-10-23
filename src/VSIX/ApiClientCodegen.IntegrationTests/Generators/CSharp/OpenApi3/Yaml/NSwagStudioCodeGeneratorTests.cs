using ApiClientCodeGen.Tests.Common.Build;
using ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3.Yaml;
using Rapicgen.Core;
using FluentAssertions;
using Xunit;

namespace Rapicgen.IntegrationTests.Generators.CSharp.OpenApi3.Yaml
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagStudioCodeGeneratorTests : IClassFixture<NSwagStudioCodeGeneratorFixture>
    {
        private readonly string code;

        public NSwagStudioCodeGeneratorTests(NSwagStudioCodeGeneratorFixture fixture)
        {
            code = fixture.Code;
        }

        [Fact]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetCoreApp,
                    code,
                    SupportedCodeGenerator.NSwagStudio)
                .Should()
                .BeTrue();

        [Fact]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetStandardLibrary,
                    code,
                    SupportedCodeGenerator.NSwagStudio)
                .Should()
                .BeTrue();
    }
}