using ApiClientCodeGen.Tests.Common;
using AutoFixture;
using Rapicgen.Core;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Generators;
using FluentAssertions;
using Moq;
using Xunit;

namespace Rapicgen.Tests.Generators
{
    public class CodeGeneratorTests : TestWithResources
    {
        private readonly Mock<IProgressReporter> mock;
        private readonly CodeGenerator sut;

        public CodeGeneratorTests()
        {
            mock = new Mock<IProgressReporter>();

            var fixture = new Fixture();
            sut = new TestCodeGenerator(
                SwaggerJsonFilename,
                fixture.Create<string>());

            ActionExtensions.SafeInvoke(() => sut.GenerateCode(mock.Object));
        }

        [Fact]
        public void GenerateCode_ReportsProgress()
            => mock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(), It.IsAny<uint>()));

        [Fact]
        public void GetsCommand() 
            => ((TestCodeGenerator) sut).GetCommandCalled.Should().BeGreaterThan(0);

        [Fact]
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
