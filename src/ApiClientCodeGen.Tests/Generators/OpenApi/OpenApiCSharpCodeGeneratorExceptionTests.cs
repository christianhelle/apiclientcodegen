using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using FluentAssertions;


namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.OpenApi
{
    
    public class OpenApiCSharpCodeGeneratorExceptionTests
    {
        [Xunit.Fact]
        public void Constructor_Requires_SwaggerFile()
            => new Action(() => new OpenApiCSharpCodeGenerator(null, null, null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [Xunit.Fact]
        public void Constructor_Requires_DefaultNamespace()
            => new Action(() => new OpenApiCSharpCodeGenerator("", null, null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [Xunit.Fact]
        public void Constructor_Requires_Options()
            => new Action(() => new OpenApiCSharpCodeGenerator("", "", null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
    }
}