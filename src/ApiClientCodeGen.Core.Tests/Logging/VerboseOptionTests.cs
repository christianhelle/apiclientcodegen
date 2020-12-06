using ApiClientCodeGen.CLI.Logging;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests.Logging
{
    public class VerboseOptionTests
    {
        [Theory]
        [InlineData("-v")]
        [InlineData("--verbose")]
        public void Constructor_Sets_Enabled_True_For_Valid_Args(string argument)
            => new VerboseOption(new[] {argument})
                .Enabled
                .Should()
                .BeTrue();

        [Theory, AutoData]
        public void Constructor_Sets_Enabled_False_For_Invalid_Args(string argument)
            => new VerboseOption(new[] {argument})
                .Enabled
                .Should()
                .BeFalse();
    }
}