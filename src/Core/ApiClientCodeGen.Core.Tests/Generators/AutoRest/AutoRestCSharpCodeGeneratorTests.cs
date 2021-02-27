using ApiClientCodeGen.Tests.Common;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using Moq;
using NSwag;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.AutoRest
{
    public class AutoRestCSharpCodeGeneratorTests : TestWithResources
    {
        [Theory, AutoMoqData]
        public void Reads_AddCredentials_From_Options(
            [Frozen] IAutoRestOptions options,
            [Frozen] IOpenApiDocumentFactory factory,
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress)
        {
            ArrangeOpenApiDocumentFactory(factory);
            sut.GenerateCode(progress);
            Mock.Get(options).Verify(c => c.AddCredentials, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_ClientSideValidation_From_Options(
            [Frozen] IAutoRestOptions options,
            [Frozen] IOpenApiDocumentFactory factory,
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress)
        {
            ArrangeOpenApiDocumentFactory(factory);
            sut.GenerateCode(progress);
            Mock.Get(options).Verify(c => c.ClientSideValidation, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_OverrideClientName_From_Options(
            [Frozen] IAutoRestOptions options,
            [Frozen] IOpenApiDocumentFactory factory,
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress)
        {
            ArrangeOpenApiDocumentFactory(factory);
            sut.GenerateCode(progress);
            Mock.Get(options).Verify(c => c.OverrideClientName, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_SyncMethods_From_Options(
            [Frozen] IAutoRestOptions options,
            [Frozen] IOpenApiDocumentFactory factory,
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress)
        {
            ArrangeOpenApiDocumentFactory(factory);
            sut.GenerateCode(progress);
            Mock.Get(options).Verify(c => c.SyncMethods, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_UseDateTimeOffset_From_Options(
            [Frozen] IAutoRestOptions options,
            [Frozen] IOpenApiDocumentFactory factory,
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress)
        {
            ArrangeOpenApiDocumentFactory(factory);
            sut.GenerateCode(progress);
            Mock.Get(options).Verify(c => c.UseDateTimeOffset, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_UseInternalConstructors_From_Options(
            [Frozen] IAutoRestOptions options,
            [Frozen] IOpenApiDocumentFactory factory,
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress)
        {
            ArrangeOpenApiDocumentFactory(factory);
            sut.GenerateCode(progress);
            Mock.Get(options).Verify(c => c.UseInternalConstructors, Times.AtLeastOnce);
        }

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
            AutoRestCSharpCodeGenerator sut,
            IProgressReporter progress,
            ProcessLaunchException exception)
        {
            Mock.Get(processLauncher)
                .Setup(
                    c => c.Start(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .Throws(exception);

            ArrangeOpenApiDocumentFactory(factory);

            try
            {
                sut.GenerateCode(progress);
            }
            catch (ProcessLaunchException)
            {
            }

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