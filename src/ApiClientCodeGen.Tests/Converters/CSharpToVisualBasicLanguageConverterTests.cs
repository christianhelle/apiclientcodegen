using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters;
using FluentAssertions;


namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Converters
{
    
    public class CSharpToVisualBasicLanguageConverterTests
    {
        [Xunit.Fact]
        public async Task ConvertShouldNotBeNullOrWhiteSpaceAsync()
        {
            const string code = "namespace X.X.X { public class Foo { } }";
            var sut = new CSharpToVisualBasicLanguageConverter();
            (await sut.ConvertAsync(code)).Should().NotBeNullOrWhiteSpace();
        }
    }
}
