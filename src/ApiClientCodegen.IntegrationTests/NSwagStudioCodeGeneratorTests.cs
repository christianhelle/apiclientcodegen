using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.nswag")]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagStudioCodeGeneratorTests
    {
        [TestMethod]
        public void IntegrationTest_Generate_Code_Using_NSwagStudio()
            => new NSwagStudioCodeGenerator(
                    Path.GetFullPath("Swagger.nswag"))
                .GenerateCode(new Mock<IVsGeneratorProgress>().Object)
                .Should()
                .BeNull();
        
        [TestMethod]
        public async Task IntegrationTest_Generate_Code_Using_NSwagStudio_From_SwaggerSpec()
        {
            var contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                File.ReadAllText("Swagger.json"),
                "https://petstore.swagger.io/v2/swagger.json");

            File.WriteAllText("Petstore.nswag", contents);
            new NSwagStudioCodeGenerator(
                    Path.GetFullPath("Petstore.nswag"))
                .GenerateCode(new Mock<IVsGeneratorProgress>().Object)
                .Should()
                .BeNull();

            File.Exists("PetstoreClient.cs")
                .Should()
                .BeTrue();
        }
    }
}
