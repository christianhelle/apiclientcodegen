using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.Core.Logging;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Logging
{
    public class SupportKeyInitializerTests
    {
        [Theory, AutoMoqData]
        public void Should_Add_SupportKey(
            SupportKeyInitializer sut,
            FakeTelemetry telemetry)
        {
            sut.Initialize(telemetry);
            telemetry.Properties.Should().ContainKey("support-key");
        }
    }
}