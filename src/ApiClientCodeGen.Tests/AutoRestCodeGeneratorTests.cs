using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.AutoRest;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class AutoRestCodeGeneratorTests
    {
        [TestMethod]
        public void Can_Generate_Code_Using_AutoRest()
            => new AutoRestCSharpGenerator(
                    Path.GetFullPath("Swagger.json"), 
                    GetType().Namespace)
                .GenerateCode()
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
