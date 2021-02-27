using ApiClientCodeGen.Tests.Common;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using Moq;
using NSwag;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.AutoRest
{
    public class AutoRestCSharpCodeGeneratorTests : TestWithResources
    {
        [Theory, AutoMoqData]
        public void Updates_Progress(
            [Frozen] IOpenApiDocumentFactory factory,
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress)
        {
            ArrangeOpenApiDocumentFactory(factory);
            sut.GenerateCode(progress);
            Mock.Get(progress)
                .Verify(
                    c => c.Progress(
                        It.IsAny<uint>(),
                        It.IsAny<uint>()),
                    Times.Exactly(5));
        }

        [Theory, AutoMoqData]
        public void Parses_OpenApi_v3(
            [Frozen] IOpenApiDocumentFactory factory,
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress)
        {
            ArrangeOpenApiDocumentFactory(factory, SwaggerV3JsonFilename);
            sut.GenerateCode(progress);
            Mock.Get(progress)
                .Verify(
                    c => c.Progress(
                        It.IsAny<uint>(),
                        It.IsAny<uint>()),
                    Times.Exactly(5));
        }

        [Theory, AutoMoqData]
        public void Tries_Again_Upon_ProcessLaunchException_For_OpenApi_v2(
            [Frozen] IOpenApiDocumentFactory factory,
            [Frozen] IProcessLauncher processLauncher,
            [Frozen] IAutoRestArgumentProvider argumentProvider,
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress,
            ProcessLaunchException exception,
            string arguments)
        {
            arguments += "--version=[some version]";
            
            Mock.Get(processLauncher)
                .Setup(
                    c => c.Start(
                        It.IsAny<string>(),
                        arguments,
                        It.IsAny<string>()))
                .Throws(exception);

            Mock.Get(argumentProvider)
                .Setup(
                    c => c.GetLegacyArguments(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .Returns(arguments);

            ArrangeOpenApiDocumentFactory(factory);
            sut.GenerateCode(progress);

            Mock.Get(processLauncher)
                .Verify(
                    c => c.Start(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()),
                    Times.Exactly(2));
        }

        private void ArrangeOpenApiDocumentFactory(
            IOpenApiDocumentFactory factory,
            string swaggerFile = null)
        {
            Mock.Get(factory)
                .Setup(
                    c => c.GetDocumentAsync(
                        It.IsAny<string>()))
                .Returns(OpenApiDocument.FromFileAsync(swaggerFile ?? SwaggerJsonFilename));
        }
    }
}