using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Logging;

public class VisualStudioVersionInitializerTests
{
    [Theory, AutoMoqData]
    public void Should_Add_VisualStudioVersion(
        VisualStudioVersionInitializer sut,
        FakeTelemetry telemetry)
    {
        sut.Initialize(telemetry);
        telemetry.Properties.Should().ContainKey("visual-studio-version");
    }
}