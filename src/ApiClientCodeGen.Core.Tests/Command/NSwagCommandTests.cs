using System;
using System.Threading.Tasks;
using ApiClientCodeGen.CLI.Commands;
using ApiClientCodeGen.CLI.Logging;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests.Command
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
            IConsoleOutput console,
            IProgressReporter progressReporter,
            IOpenApiDocumentFactory openApiDocumentFactory,
            INSwagOptions options,
            INSwagCodeGeneratorFactory codeGeneratorFactory,
            ICodeGenerator generator,
            string outputFile,
            string code)
        {
            var sut = new NSwagCommand(
                console,
                progressReporter,
                openApiDocumentFactory,
                options,
                codeGeneratorFactory);
            sut.OutputFile = outputFile;
            
            Mock.Get(generator)
                .Setup(c => c.GenerateCode(progressReporter))
                .Returns(code);
            
            new Func<int>(sut.OnExecute).Should().NotThrow();
        }
    }
}