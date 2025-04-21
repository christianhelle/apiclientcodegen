using ApiClientCodeGen.Tests.Common;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Rapicgen.Core;
using Rapicgen.Core.Generators.OpenApi;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.OpenApi
{
    public class OpenApiCSharpCodeGeneratorTests : TestWithResources
    {
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
