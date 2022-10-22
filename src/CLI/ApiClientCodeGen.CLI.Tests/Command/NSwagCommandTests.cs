using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Rapicgen.CLI.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using FluentAssertions;
using Moq;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class NSwagCommandTests
    {   
        [Theory, AutoMoqData]
        public void DefaultNamespace_Should_NotBeNullOrWhiteSpace(NSwagCommand sut)
            => sut.DefaultNamespace.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void SwaggerFile_Should_NotBeNullOrWhiteSpace(NSwagCommand sut)
            => sut.SwaggerFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OutputFile_Should_NotBeNullOrWhiteSpace(NSwagCommand sut)
            => sut.OutputFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void CreateGenerator_Should_NotNull(NSwagCommand sut)
            => sut.CreateGenerator().Should().NotBeNull();

        [Theory, AutoMoqData]
        public void OnExecuteAsync_Should_NotThrow(
            [Frozen] IProgressReporter progressReporter,
            [Frozen] ICodeGenerator generator,
            NSwagCommand sut,
            string outputFile,
            string code)
        {
            sut.OutputFile = outputFile;
            
            Mock.Get(generator)
                .Setup(c => c.GenerateCode(progressReporter))
                .Returns(code);
            
            new Func<int>(sut.OnExecute).Should().NotThrow();
        }
    }
}