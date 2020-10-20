using System;
using AutoFixture.Xunit2;
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
                .EndsWithAny(new[] { "json", "yaml", "yml" }, StringComparison.OrdinalIgnoreCase)
                .Should()
                .BeTrue();

        [Fact]
        public void EndsWithAny_Throws_ArgumentNullException()
            => new Action(() => ((string)null).EndsWithAny())
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [Theory]
        [AutoData]
        public void EndsWithAny_Null_Words_Throws_ArgumentNullException(string sut)
            => new Action(() => sut.EndsWithAny(null))
                .Should()
                .ThrowExactly<ArgumentNullException>();
    }
}