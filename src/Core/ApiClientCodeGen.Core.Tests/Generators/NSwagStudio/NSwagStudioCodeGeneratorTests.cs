using System.IO;
using ApiClientCodeGen.Tests.Common;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwagStudio;
using Rapicgen.Core.Options.General;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.NSwagStudio
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagStudioCodeGeneratorTests : TestWithResources
    {
        [Theory, AutoMoqData]
        public void NSwagStudio_GenerateCode_Returns_Empty_String(
            IProgressReporter progressReporter,
            NSwagStudioCodeGenerator sut)
        {
            sut.GenerateCode(progressReporter)
                .Should()
                .BeEmpty();
        }

        [Theory, AutoMoqData]
        public void Reads_NSwagPath_From_Options(
            [Frozen] IGeneralOptions options,
            IProgressReporter progressReporter,
            NSwagStudioCodeGenerator sut)
        {
            sut.GenerateCode(progressReporter);
            Mock.Get(options).Verify(c => c.NSwagPath);
        }

        [Theory, AutoMoqData]
        public void Launches_NSwag(
            [Frozen] IProcessLauncher process,
            IProgressReporter progressReporter,
            NSwagStudioCodeGenerator sut)
        {
            sut.GenerateCode(progressReporter);
            Mock.Get(process)
                .Verify(
                    c => c.Start(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()));
        }
    }
}