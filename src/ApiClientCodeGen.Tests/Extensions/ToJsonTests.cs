using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    [TestClass]
    public class ToJsonTests
    {
        string json;
        [TestInitialize]
        public void Init()
        {
            json = new
            {
                Str1 = Test.CreateAnnonymous<string>(),
                Str2 = Test.CreateAnnonymous<string>(),
                Str3 = Test.CreateAnnonymous<string>(),
                Null = (object)null
            }.ToJson();
        }

        [TestMethod]
        public void NotNull()
            => json.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void Is_CamelCase()
            => json.Should().NotContain("Str").And.Contain("str");

        [TestMethod]
        public void Ignores_Null_Values()
            => json.Should().NotContain("null");
    }
}