using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.Core.Logging;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Logging
{
    public class SupportInformationInitializerTests
    {
        [Theory, AutoMoqData]
        public void Should_Add_SupportKey(
            SupportInformationInitializer sut,
            FakeTelemetry telemetry)
        {
            sut.Initialize(telemetry);
            telemetry.Properties.Should().ContainKey("support-key");
        }

        [Theory, AutoMoqData]
        public void Should_Add_Version(
            SupportInformationInitializer sut,
            FakeTelemetry telemetry)
        {
            sut.Initialize(telemetry);
            telemetry.Properties.Should().ContainKey("version");
        }
    }
}
