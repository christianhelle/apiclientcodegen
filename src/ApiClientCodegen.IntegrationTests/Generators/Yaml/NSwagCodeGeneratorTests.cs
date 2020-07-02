using System;
using FluentAssertions;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.Yaml
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagCodeGeneratorYamlTests : IClassFixture<NSwagCodeGeneratorFixture>
    {
        private readonly NSwagCodeGeneratorFixture fixture;

        public NSwagCodeGeneratorYamlTests(NSwagCodeGeneratorFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public void GenerateCode_Throws_NotSupportedException()
            => new Action(() => fixture.GenerateCode())
                .Should()
                .ThrowExactly<NotSupportedException>();
    }
}
