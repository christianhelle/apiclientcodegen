using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class CodeGeneratorTests
    {
        private Mock<IVsGeneratorProgress> mock;
        private CodeGenerator sut;

        [TestInitialize]
        public void Init()
        {
            mock = new Mock<IVsGeneratorProgress>();

            var fixture = new Fixture();
            sut = new TestCodeGenerator(
                "Swagger.json",
                fixture.Create<string>());

            try
            {
                sut.GenerateCode(mock.Object);
            }
            catch
            {
                // ignored
            }
        }

        [TestMethod]
        public void GenerateCode_ReportsProgress()
            => mock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()));

        [TestMethod]
        public void GetsCommand() 
            => ((TestCodeGenerator) sut).GetCommandCalled.Should().BeGreaterThan(0);

        [TestMethod]
        public void GetsArgument() 
            => ((TestCodeGenerator) sut).GetArgumentsCalled.Should().BeGreaterThan(0);

        internal class TestCodeGenerator : CodeGenerator
        {
            public TestCodeGenerator(string swaggerFile, string defaultNamespace)
                : base(swaggerFile, defaultNamespace)
            {
            }

            protected override string GetArguments(string outputFile)
            {
                GetArgumentsCalled++;
                return string.Empty;
            }

            protected override string GetCommand()
            {
                GetCommandCalled++;
                return string.Empty;
            }

            public int GetArgumentsCalled { get; private set; }

            public int GetCommandCalled { get; private set; }
        }
    }
}
