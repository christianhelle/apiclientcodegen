using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.OpenApi
{
    [TestClass]
    public class OpenApiCSharpCodeGeneratorExceptionTests
    {
        [TestMethod]
        public void Constructor_Requires_SwaggerFile()
            => new Action(() => new OpenApiCSharpCodeGenerator(null, null, null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [TestMethod]
        public void Constructor_Requires_DefaultNamespace()
            => new Action(() => new OpenApiCSharpCodeGenerator("", null, null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [TestMethod]
        public void Constructor_Requires_Options()
            => new Action(() => new OpenApiCSharpCodeGenerator("", "", null, new ProcessLauncher()))
                .Should()
                .ThrowExactly<ArgumentNullException>();
    }
}