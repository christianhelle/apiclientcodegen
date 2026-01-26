using System;
using ApiClientCodeGen.Tests.Common.Fixtures.VisualBasic;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests.Generators.VisualBasic
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class OpenApiVisualBasicCodeGeneratorTests : IClassFixture<OpenApiVisualBasicCodeGeneratorFixture>
    {
        private readonly OpenApiVisualBasicCodeGeneratorFixture fixture;

        public OpenApiVisualBasicCodeGeneratorTests(OpenApiVisualBasicCodeGeneratorFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [SkippableFact(typeof(NotSupportedException))]
        public void OpenApi_Generated_CSharp_Code_NotNullOrWhitespace()
            => fixture.CSharpCode.Should().NotBeNullOrWhiteSpace();

        [SkippableFact(typeof(NotSupportedException))]
        public void OpenApi_Generated_VisualBasic_Code_NotNullOrWhitespace()
            => fixture.VisualBasicCode.Should().NotBeNullOrWhiteSpace();

        [SkippableFact(typeof(NotSupportedException))]
        public void OpenApi_Reports_Progress()
            => fixture.ProgressReporterMock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [SkippableFact(typeof(NotSupportedException))]
        public void VisualBasic_Code_Contains_Class()
            => fixture.VisualBasicCode.Should().Contain("Class ");

        [SkippableFact(typeof(NotSupportedException))]
        public void VisualBasic_Code_Contains_End_Class()
            => fixture.VisualBasicCode.Should().Contain("End Class");
    }
}
