using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class DeploymentItemTests
    {        
        [TestMethod]
        public void Can_Read_Test_Swagger_Spec()
            => File.ReadAllText("Swagger.json")
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
