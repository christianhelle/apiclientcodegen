using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests
{
    public class VerboseOptionTests
    {
        [Theory]
        [InlineData("-v")]
        [InlineData("--verbose")]
        public void Parse_Returns_True_For_v(string argument)
            => VerboseOption
                .Parse(argument)
                .Should()
                .BeTrue();
    }
}
