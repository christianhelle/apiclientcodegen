using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.OpenApi;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiCodeGeneratorTests
    {
        [TestMethod]
        public void IntegrationTest_Generate_Code_Using_OpenApi()
            => new OpenApiCSharpCodeGenerator(
                    Path.GetFullPath("Swagger.json"), 
                    GetType().Namespace)
                .GenerateCode(null)
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
