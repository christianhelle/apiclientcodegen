using ApiClientCodeGen.Tests.Common;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Rapicgen.Core;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Options.General;
using Moq;
using Xunit;

namespace Rapicgen.Tests.Generators.Swagger
{
    public class SwaggerCSharpCodeGeneratorTests : TestWithResources
    {
        [Theory, AutoMoqData]
        public void Reads_SwaggerCodegenPath(
            [Frozen] IGeneralOptions options,
            [Frozen] IProgressReporter progressReporter,
            SwaggerCSharpCodeGenerator sut)
        {
            sut.GenerateCode(progressReporter);
            Mock.Get(options).Verify(c => c.SwaggerCodegenPath);
        }

        [Theory, AutoMoqData]
        public void Updates_Progress(
            [Frozen] IProgressReporter progressReporter,
            SwaggerCSharpCodeGenerator sut)
        {
            sut.GenerateCode(progressReporter);
            Mock.Get(progressReporter).Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()),
                Times.Exactly(5));
        }
    }
}