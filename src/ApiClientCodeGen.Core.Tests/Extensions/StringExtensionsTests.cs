using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Extensions
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

        [Theory]
        [InlineData("Swagger.json")]
        [InlineData("Swagger.yaml")]
        [InlineData("Swagger.yml")]
        public void EndsWithAny_IgnoreCase_Returns_True(string filename)
            => filename
                .EndsWithAny(new[] {"json", "yaml", "yml"}, StringComparison.OrdinalIgnoreCase)
                .Should()
                .BeTrue();
    }
}