using ApiClientCodeGen.Tests.Common.Build;
using ApiClientCodeGen.Tests.Common.Fixtures.Yaml;
using FluentAssertions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests.Generators.CSharp.Yaml
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class KiotaCodeGeneratorTests : IClassFixture<KiotaCodeGeneratorFixture>
    {
        private readonly string code;

        public KiotaCodeGeneratorTests(KiotaCodeGeneratorFixture fixture)
        {
            code = fixture.Code;
        }

        [SkippableFact(typeof(ProcessLaunchException))]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetCoreApp,
                    code,
                    SupportedCodeGenerator.Kiota)
                .Should()
                .BeTrue();

        [SkippableFact(typeof(ProcessLaunchException))]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetStandardLibrary,
                    code,
                    SupportedCodeGenerator.Kiota)
                .Should()
                .BeTrue();
    }
}