using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ApiClientCodeGen.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class AutoRestCodeGeneratorTests
    {
        [TestMethod]
        public void Can_Read_Swagger_Spec()
            => File.ReadAllText("Swagger.json")
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod]
        public void Can_Generate_CSharp_Code()
            => new AutoRestCSharpGenerator(
                    Path.GetFullPath("Swagger.json"), 
                    GetType().Namespace)
                .GenerateCode()
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
