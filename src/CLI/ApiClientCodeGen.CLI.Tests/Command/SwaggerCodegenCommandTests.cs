using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Rapicgen.CLI.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using FluentAssertions;
using Moq;
using Rapicgen.CLI.Commands.CSharp;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class SwaggerCodegenCommandTests
    {   
        [Theory, AutoMoqData]
        public void DefaultNamespace_Should_NotBeNullOrWhiteSpace(SwaggerCodegenCommandSettings settings)
            => settings.DefaultNamespace.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void SwaggerFile_Should_NotBeNullOrWhiteSpace(SwaggerCodegenCommandSettings settings)
            => settings.SwaggerFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OutputFile_Should_NotBeNullOrWhiteSpace(SwaggerCodegenCommandSettings settings)
            => settings.OutputFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void CreateGenerator_Should_NotNull(SwaggerCodegenCommand sut, SwaggerCodegenCommandSettings settings)
            => sut.CreateGenerator(settings).Should().NotBeNull();        [Theory, AutoMoqData]
        public void Execute_Should_NotThrow(
            [Frozen] IProgressReporter progressReporter,
            [Frozen] ICodeGenerator generator,
            SwaggerCodegenCommand sut,
            SwaggerCodegenCommandSettings settings,
            string code)
        {
            Mock.Get(generator)
                .Setup(c => c.GenerateCode(progressReporter))
                .Returns(code);
            
            new Func<int>(() => sut.Execute(null, settings)).Should().NotThrow();
        }
    }
}