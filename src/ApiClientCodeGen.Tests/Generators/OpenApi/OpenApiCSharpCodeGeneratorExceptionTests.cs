using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.OpenApi;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.OpenApi
{
    [TestClass]
    public class OpenApiCSharpCodeGeneratorExceptionTests
    {
        [TestMethod]
        public void Constructor_Requires_SwaggerFile()
            => new Action(() => new OpenApiCSharpCodeGenerator(null, null, null))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [TestMethod]
        public void Constructor_Requires_DefaultNamespace()
            => new Action(() => new OpenApiCSharpCodeGenerator("", null, null))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [TestMethod]
        public void Constructor_Requires_Options()
            => new Action(() => new OpenApiCSharpCodeGenerator("", "", null))
                .Should()
                .ThrowExactly<ArgumentNullException>();
    }
}