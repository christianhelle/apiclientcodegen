using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagCodeGeneratorTests
    {
        [TestMethod]
        public void IntegrationTest_Generate_Code_Using_NSwag()
            => new NSwagCSharpCodeGenerator(
                    Path.GetFullPath("Swagger.json"), 
                    GetType().Namespace)
                .GenerateCode()
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
