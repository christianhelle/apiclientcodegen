using System.Threading.Tasks;
using Rapicgen.Converters;
using FluentAssertions;


namespace Rapicgen.Tests.Converters
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
