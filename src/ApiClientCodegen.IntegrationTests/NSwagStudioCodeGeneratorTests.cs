using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.nswag")]
    public class NSwagStudioCodeGeneratorTests
    {
        [TestMethod]
        public void IntegrationTest_Generate_Code_Using_NSwagStudio()
            => new NSwagStudioCodeGenerator(
                    Path.GetFullPath("Swagger.nswag"))
                .GenerateCode(new Mock<IVsGeneratorProgress>().Object)
                .Should()
                .BeNull();
    }
}
