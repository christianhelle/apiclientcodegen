using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    
    public class ToJsonTests
    {
        string json;

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

        [Xunit.Fact]
        public void NotNull()
            => json.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void Is_CamelCase()
            => json.Should().NotContain("Str").And.Contain("str");

        [Xunit.Fact]
        public void Ignores_Null_Values()
            => json.Should().NotContain("null");
    }
}