using System.IO;
using FluentAssertions;


namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    
    // [DeploymentItem("Resources/Swagger.json")]
    public class DeploymentItemTests
    {        
        [Xunit.Fact]
        public void Can_Read_Test_Swagger_Spec()
            => File.ReadAllText("Swagger.json")
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
