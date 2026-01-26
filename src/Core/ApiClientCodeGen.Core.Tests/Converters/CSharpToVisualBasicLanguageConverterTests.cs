using System.Threading.Tasks;
using FluentAssertions;
using Rapicgen.Core.Converters;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Converters
{
    public class CSharpToVisualBasicLanguageConverterTests
    {
        private readonly CSharpToVisualBasicLanguageConverter sut = new CSharpToVisualBasicLanguageConverter();

        [Fact]
        public async Task ConvertAsync_WithSimpleClass_ReturnsVbCode()
        {
            // Arrange
            const string csharpCode = @"
namespace TestNamespace
{
    public class TestClass
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}";

            // Act
            var result = await sut.ConvertAsync(csharpCode);

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Contain("Class TestClass");
            result.Should().Contain("End Class");
            result.Should().Contain("Property Name");
            result.Should().Contain("Property Age");
        }

        [Fact]
        public async Task ConvertAsync_WithInterface_ReturnsVbCode()
        {
            // Arrange
            const string csharpCode = @"
namespace TestNamespace
{
    public interface ITestInterface
    {
        void DoSomething();
        string GetName();
    }
}";

            // Act
            var result = await sut.ConvertAsync(csharpCode);

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Contain("Interface ITestInterface");
            result.Should().Contain("End Interface");
        }

        [Fact]
        public async Task ConvertAsync_WithEmptyString_ReturnsNonNullResult()
        {
            // Arrange
            const string csharpCode = "";

            // Act
            var result = await sut.ConvertAsync(csharpCode);

            // Assert
            // The converter produces a wrapper even for empty input
            result.Should().NotBeNull();
        }
    }
}
