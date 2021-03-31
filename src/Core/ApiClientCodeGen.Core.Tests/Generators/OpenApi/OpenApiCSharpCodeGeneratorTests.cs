using ApiClientCodeGen.Tests.Common;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.OpenApi
{
    public class OpenApiCSharpCodeGeneratorTests : TestWithResources
    {
        [Theory, AutoMoqData]
        public void Reads_SwaggerCodegenPath(
            [Frozen] IGeneralOptions options,
            IProgressReporter progressReporter,
            OpenApiCSharpCodeGenerator sut)
        {
            sut.GenerateCode(progressReporter);
            Mock.Get(options).Verify(c => c.OpenApiGeneratorPath);
        }

        [Theory, AutoMoqData]
        public void Updates_Progress(
            [Frozen] IProgressReporter progressReporter,
            OpenApiCSharpCodeGenerator sut)
        {
            sut.GenerateCode(progressReporter);
            Mock.Get(progressReporter)
                .Verify(
                    c => c.Progress(
                        It.IsAny<uint>(),
                        It.IsAny<uint>()),
                    Times.Exactly(5));
        }
    }
}
