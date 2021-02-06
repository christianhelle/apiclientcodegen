using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.Swagger;
using FluentAssertions;

namespace ApiClientCodeGen.Core.Tests.Generators.Swagger
{
    
    public class SwaggerCSharpCodeGeneratorExceptionTests
    {
        [Xunit.Fact]
        public void Constructor_Requires_SwaggerFile()
            => new Action(() => new SwaggerCSharpCodeGenerator(null, null, null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [Xunit.Fact]
        public void Constructor_Requires_DefaultNamespace()
            => new Action(() => new SwaggerCSharpCodeGenerator("", null, null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [Xunit.Fact]
        public void Constructor_Requires_Options()
            => new Action(() => new SwaggerCSharpCodeGenerator("", "", null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
    }
}