using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Converters
{
    [TestClass]
    public class CSharpToVisualBasicLanguageConverterTests
    {
        [TestMethod, Xunit.Fact]
        public async Task ConvertShouldNotBeNullOrWhiteSpaceAsync()
        {
            const string code = "namespace X.X.X { public class Foo { } }";
            var sut = new CSharpToVisualBasicLanguageConverter();
            (await sut.ConvertAsync(code)).Should().NotBeNullOrWhiteSpace();
        }
    }
}
