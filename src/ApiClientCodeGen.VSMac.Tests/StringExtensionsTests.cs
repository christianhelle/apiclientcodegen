using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.VSMac.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("Swagger.json")]
        [InlineData("Swagger.yaml")]
        [InlineData("Swagger.yml")]
        public void EndsWithAny_Returns_True(string filename)
            => filename
                .EndsWithAny("json", "yaml", "yml")
                .Should()
                .BeTrue();
    }
}