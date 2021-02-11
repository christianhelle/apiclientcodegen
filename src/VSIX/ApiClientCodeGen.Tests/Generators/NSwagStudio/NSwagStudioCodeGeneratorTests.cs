using System.IO;
using ApiClientCodeGen.Tests.Common;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwagStudio
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagStudioCodeGeneratorTests : TestWithResources
    {
        [Theory, AutoMoqData]
        public void NSwagStudio_GenerateCode_Returns_Null(
            IProcessLauncher process,
            IGeneralOptions options,
            IProgressReporter progressReporter)
        {
            new NSwagStudioCodeGenerator(
                    Path.GetFullPath(SwaggerNSwagFilename),
                    options,
                    process)
                .GenerateCode(progressReporter)
                .Should()
                .BeNull();
        }

        [Theory, AutoMoqData]
        public void Reads_NSwagPath_From_Options(
            IProcessLauncher process,
            IGeneralOptions options,
            IProgressReporter progressReporter)
        {
            new NSwagStudioCodeGenerator(
                    Path.GetFullPath(SwaggerNSwagFilename),
                    options,
                    process,
                    true)
                .GenerateCode(progressReporter);
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
