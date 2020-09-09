using System;
using System.Threading.Tasks;
using ApiClientCodeGen.CLI.Commands;
using ApiClientCodeGen.CLI.Logging;
using ApiClientCodeGen.CLI.Tests.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests.Command
{
    public class AutoRestCommandTests
    {
        [Theory, AutoMoqData]
        public void DefaultNamespace_Should_NotBeNullOrWhiteSpace(AutoRestCommand sut)
            => sut.DefaultNamespace.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void SwaggerFile_Should_NotBeNullOrWhiteSpace(AutoRestCommand sut)
            => sut.SwaggerFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OutputFile_Should_NotBeNullOrWhiteSpace(AutoRestCommand sut)
            => sut.OutputFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void CreateGenerator_Should_NotNull(AutoRestCommand sut)
            => sut.CreateGenerator().Should().NotBeNull();

        [Theory, AutoMoqData]
        public void OnExecuteAsync_Should_NotThrow(
            IConsoleOutput console,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IProgressReporter progressReporter,
            IAutoRestCodeGeneratorFactory factory,
            ICodeGenerator generator,
            IOpenApiDocumentFactory documentFactory,
            string outputFile,
            string code)
        {
            var sut = new AutoRestCommand(
                console,
                options,
                processLauncher,
                progressReporter,
                factory,
                documentFactory)
            {
                OutputFile = outputFile
            };
            Mock.Get(generator).Setup(c => c.GenerateCode(progressReporter)).Returns(code);
            new Func<Task>(sut.OnExecuteAsync).Should().NotThrow();
        }
    }
}
