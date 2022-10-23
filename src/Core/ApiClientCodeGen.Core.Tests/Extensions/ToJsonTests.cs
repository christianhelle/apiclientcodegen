using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Extensions
{
    
    public class ToJsonTests
    {
        private readonly string json;

        public ToJsonTests()
        {
            json = new
            {
                Str1 = Test.CreateAnnonymous<string>(),
                Str2 = Test.CreateAnnonymous<string>(),
                Str3 = Test.CreateAnnonymous<string>(),
                Null = (object)null
            }.ToJson();
        }

        [Fact]
        public void NotNull()
            => json.Should().NotBeNullOrWhiteSpace();

        [Fact]
        public void Is_CamelCase()
            => json.Should().NotContain("Str").And.Contain("str");

        [Fact]
        public void Ignores_Null_Values()
            => json.Should().NotContain("null");
    }
}