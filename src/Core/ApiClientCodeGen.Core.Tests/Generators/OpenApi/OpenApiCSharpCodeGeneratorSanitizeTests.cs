using System.Reflection;
using Rapicgen.Core.Generators.OpenApi;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.OpenApi
{
    public class OpenApiCSharpCodeGeneratorSanitizeTests
    {
        [Fact]
        public void Sanitize_Removes_SystemNetMime_Using_Statement()
        {
            // Arrange
            const string codeWithMimeUsing = "using System;\nusing System.Net.Mime;\nnamespace Test {}";
            const string expectedResult = "using System;\n\nnamespace Test {}";
            
            // Get the private Sanitize method through reflection
            var sanitizeMethod = typeof(OpenApiCSharpCodeGenerator)
                .GetMethod("Sanitize", BindingFlags.NonPublic | BindingFlags.Static);
            
            // Act
            var result = sanitizeMethod?.Invoke(null, new object[] { codeWithMimeUsing }) as string;
            
            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}