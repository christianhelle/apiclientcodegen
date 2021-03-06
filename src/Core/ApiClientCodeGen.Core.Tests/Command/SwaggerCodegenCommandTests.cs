using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Commands;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Command
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
            IDependencyInstaller dependencyInstaller,
            string code)
        {
            var sut = new SwaggerCodegenCommand(
                console,
                progressReporter, 
                options,
                processLauncher, 
                codeGeneratorFactory,
                dependencyInstaller);
            
            Mock.Get(generator)
                .Setup(c => c.GenerateCode(progressReporter))
                .Returns(code);
            
            new Func<int>(sut.OnExecute).Should().NotThrow();
        }
    }
}