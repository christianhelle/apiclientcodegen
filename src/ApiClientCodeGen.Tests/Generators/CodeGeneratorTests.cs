using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;

using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators
{
    
    // [DeploymentItem("Resources/Swagger.json")]
    public class CodeGeneratorTests
    {
        private Mock<IProgressReporter> mock;
        private CodeGenerator sut;

        public CodeGeneratorTests()
        {
            mock = new Mock<IProgressReporter>();

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

        [Xunit.Fact]
        public void GenerateCode_ReportsProgress()
            => mock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(), It.IsAny<uint>()));

        [Xunit.Fact]
        public void GetsCommand() 
            => ((TestCodeGenerator) sut).GetCommandCalled.Should().BeGreaterThan(0);

        [Xunit.Fact]
        public void GetsArgument() 
            => ((TestCodeGenerator) sut).GetArgumentsCalled.Should().BeGreaterThan(0);

        internal class TestCodeGenerator : CodeGenerator
        {
            public TestCodeGenerator(string swaggerFile, string defaultNamespace)
                : base(swaggerFile, defaultNamespace, Test.CreateDummy<IProcessLauncher>())
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
