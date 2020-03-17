using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.Swagger;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.Swagger
{
    [TestClass]
    public class SwaggerCSharpCodeGeneratorExceptionTests
    {
        [TestMethod, Xunit.Fact]
        public void Constructor_Requires_SwaggerFile()
            => new Action(() => new SwaggerCSharpCodeGenerator(null, null, null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [TestMethod, Xunit.Fact]
        public void Constructor_Requires_DefaultNamespace()
            => new Action(() => new SwaggerCSharpCodeGenerator("", null, null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [TestMethod, Xunit.Fact]
        public void Constructor_Requires_Options()
            => new Action(() => new SwaggerCSharpCodeGenerator("", "", null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
    }
}