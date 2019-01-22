using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagCodeGeneratorTests
    {
        [TestMethod]
        public void Can_Generate_Code_Using_NSwag()
            => new NSwagCSharpCodeGenerator(
                    Path.GetFullPath("Swagger.json"), 
                    GetType().Namespace)
                .GenerateCode()
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
