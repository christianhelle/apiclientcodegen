using System;
using ApiClientCodeGen.Tests.Common.Fixtures.VisualBasic;
using Rapicgen.Core;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests.Generators.VisualBasic
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagVisualBasicCodeGeneratorTests : IClassFixture<NSwagVisualBasicCodeGeneratorFixture>
    {
        private readonly NSwagVisualBasicCodeGeneratorFixture fixture;

        public NSwagVisualBasicCodeGeneratorTests(NSwagVisualBasicCodeGeneratorFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public void NSwag_Generated_CSharp_Code_NotNullOrWhitespace()
            => fixture.CSharpCode.Should().NotBeNullOrWhiteSpace();

        [Fact]
        public void NSwag_Generated_VisualBasic_Code_NotNullOrWhitespace()
            => fixture.VisualBasicCode.Should().NotBeNullOrWhiteSpace();

        [Fact]
        public void NSwag_Reports_Progress()
            => fixture.ProgressReporterMock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [Fact]
        public void VisualBasic_Code_Contains_Module_Or_Class()
            => fixture.VisualBasicCode.Should().ContainAny("Class ", "Module ");

        [Fact]
        public void VisualBasic_Code_Contains_End_Class()
            => fixture.VisualBasicCode.Should().Contain("End Class");

        [Fact]
        public void VisualBasic_Code_Does_Not_Contain_CSharp_Syntax()
        {
            // VB.NET code should not contain C# specific syntax
            fixture.VisualBasicCode.Should().NotContain("namespace ");
            fixture.VisualBasicCode.Should().NotContain("using ");
        }
    }
}
