using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.nswag")]
    [DeploymentItem("Resources/NSwagFileWithoutSwaggerJson.nswag")]
    public class NSwagStudioCodeGeneratorTests
    {
        [TestMethod]
        public void Returns_New_File_If_NSwagStudio_File_Contains_Swagger_Json()
        {
            var path = Path.GetFullPath("Resources/Swagger.nswag");
            var result = NSwagStudioCodeGenerator.TryRemoveSwaggerJsonSpec(path);
            Assert.AreNotEqual(path, result);
        }
        
        [TestMethod]
        public void Returns_New_Same_File_If_NSwagStudio_File_Doesnt_Contains_Swagger_Json()
        {
            var path = Path.GetFullPath("Resources/NSwagFileWithoutSwaggerJson.nswag");
            var result = NSwagStudioCodeGenerator.TryRemoveSwaggerJsonSpec(path);
            Assert.AreEqual(path, result);
        }
    }
}
