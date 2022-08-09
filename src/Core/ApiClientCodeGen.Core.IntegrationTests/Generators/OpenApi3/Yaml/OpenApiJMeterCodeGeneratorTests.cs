using System;
using ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3.Yaml;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests.Generators.OpenApi3.Yaml
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class OpenApiJMeterCodeGeneratorTests : IClassFixture<OpenApiJMeterCodeGeneratorFixture>
    {
        private readonly OpenApiJMeterCodeGeneratorFixture fixture;

        public OpenApiJMeterCodeGeneratorTests(OpenApiJMeterCodeGeneratorFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [SkippableFact(typeof(NotSupportedException))]
        public void OpenApi_Reports_Progres()
            => fixture.ProgressReporterMock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [SkippableFact(typeof(NotSupportedException))]
        public void Reads_JavaPath_From_Options()
            => fixture.OptionsMock.Verify(c => c.JavaPath);
    }
}