using System;
using System.Threading.Tasks;
using ApiClientCodeGen.CLI.Commands;
using ApiClientCodeGen.CLI.Logging;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests.Command
{
    public class SwaggerCodegenCommandTests
    {   
        [Theory, AutoMoqData]
        public void DefaultNamespace_Should_NotBeNullOrWhiteSpace(SwaggerCodegenCommand sut)
            => sut.DefaultNamespace.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void SwaggerFile_Should_NotBeNullOrWhiteSpace(SwaggerCodegenCommand sut)
            => sut.SwaggerFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OutputFile_Should_NotBeNullOrWhiteSpace(SwaggerCodegenCommand sut)
            => sut.OutputFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void CreateGenerator_Should_NotNull(SwaggerCodegenCommand sut)
            => sut.CreateGenerator().Should().NotBeNull();

        [Theory, AutoMoqData]
        public void OnExecuteAsync_Should_NotThrow(
            IConsoleOutput console,
            IProgressReporter progressReporter,
            IProcessLauncher processLauncher,
            IGeneralOptions options,
            ISwaggerCodegenFactory codeGeneratorFactory,
            ICodeGenerator generator,
            string code)
        {
            var sut = new SwaggerCodegenCommand(
                console,
                progressReporter, 
                options,
                processLauncher, 
                codeGeneratorFactory);
            
            Mock.Get(generator)
                .Setup(c => c.GenerateCode(progressReporter))
                .Returns(code);
            
            new Func<Task>(sut.OnExecuteAsync).Should().NotThrow();
        }
    }
}