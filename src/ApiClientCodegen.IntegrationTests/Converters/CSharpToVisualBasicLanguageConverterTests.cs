using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Converters
{
    [TestClass]
    public class CSharpToVisualBasicLanguageConverterTests
    {
        [TestMethod]
        public async Task ConvertShouldNotBeNullOrWhiteSpaceAsync()
        {
            const string code = "namespace X.X.X { public class Foo { } }";
            var sut = new CSharpToVisualBasicLanguageConverter();
            (await sut.ConvertAsync(code)).Should().NotBeNullOrWhiteSpace();
        }
    }
}
