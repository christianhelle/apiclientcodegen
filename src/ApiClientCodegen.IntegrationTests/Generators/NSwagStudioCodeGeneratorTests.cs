using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.nswag")]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagStudioCodeGeneratorTests
    {
        private Mock<IGeneralOptions> optionsMock;
        private IGeneralOptions options;

        [TestInitialize]
        public void Init()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());
            options = optionsMock.Object;
        }

        [TestMethod]
        public void NSwagStudio_Generate_Code_Using_NSwagStudio()
            => new NSwagStudioCodeGenerator(Path.GetFullPath("Swagger.nswag"), options)
                .GenerateCode(new Mock<IVsGeneratorProgress>().Object)
                .Should()
                .BeNull();
        
        [TestMethod]
        public async Task NSwagStudio_Generate_Code_Using_NSwagStudio_From_SwaggerSpec()
        {
            var contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                File.ReadAllText(Path.GetFullPath("Swagger.json")),
                "https://petstore.swagger.io/v2/swagger.json");

            File.WriteAllText("Petstore.nswag", contents);
            new NSwagStudioCodeGenerator(Path.GetFullPath("Petstore.nswag"), options)
                .GenerateCode(new Mock<IVsGeneratorProgress>().Object)
                .Should()
                .BeNull();

            File.Exists(Path.GetFullPath("PetstoreClient.cs"))
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Reads_NSwagPath_From_Options()
        {
            new NSwagStudioCodeGenerator(
                    Path.GetFullPath("Swagger.nswag"), 
                    options)
                .GenerateCode(new Mock<IVsGeneratorProgress>().Object);

            optionsMock.Verify(c => c.NSwagPath);
        }
    }
}
