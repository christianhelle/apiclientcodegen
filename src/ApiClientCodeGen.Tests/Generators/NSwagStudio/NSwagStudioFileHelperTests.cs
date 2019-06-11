using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwagStudio
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.nswag")]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagStudioFileHelperTests
    {
        private Mock<INSwagStudioOptions> mock;

        [TestInitialize]
        public async Task Init()
        {
            mock = new Mock<INSwagStudioOptions>();

            await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                File.ReadAllText("Swagger.json"),
                "https://petstore.swagger.io/v2/swagger.json",
                mock.Object);
        }

        [TestMethod]
        public void Reads_InjectHttpClient_From_Options()
            => mock.Verify(c => c.InjectHttpClient);

        [TestMethod]
        public void Reads_GenerateClientInterfaces_From_Options()
            => mock.Verify(c => c.GenerateClientInterfaces);

        [TestMethod]
        public void Reads_GenerateDtoTypes_From_Options()
            => mock.Verify(c => c.GenerateDtoTypes);

        [TestMethod]
        public void Reads_UseBaseUrl_From_Options()
            => mock.Verify(c => c.UseBaseUrl);

        [TestMethod]
        public void Reads_ClassStyle_From_Options()
            => mock.Verify(c => c.ClassStyle);

        [TestMethod]
        public void Reads_GenerateResponseClasses_From_Options()
            => mock.Verify(c => c.GenerateResponseClasses);

        [TestMethod]
        public void Reads_GenerateJsonMethods_From_Options()
            => mock.Verify(c => c.GenerateJsonMethods);

        [TestMethod]
        public void Reads_RequiredPropertiesMustBeDefined_From_Options()
            => mock.Verify(c => c.RequiredPropertiesMustBeDefined);

        [TestMethod]
        public void Reads_GenerateDefaultValues_From_Options()
            => mock.Verify(c => c.GenerateDefaultValues);

        [TestMethod]
        public void Reads_GenerateDataAnnotations_From_Options()
            => mock.Verify(c => c.GenerateDataAnnotations);
    }
}
