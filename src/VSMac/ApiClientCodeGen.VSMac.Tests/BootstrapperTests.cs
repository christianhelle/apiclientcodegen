using System.Diagnostics;
using ApiClientCodeGen.VSMac.Logging;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.VSMac.Tests
{
    public class BootstrapperTests
    {
        [Fact]
        public void Initialize_Sets_TraceListener()
        {
            Bootstrapper.Initialize();

            Trace.Listeners
                .Contains(Container.Instance.Resolve<LoggingServiceTraceListener>())
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Initialize_Twice_Does_Nothing()
        {
            Bootstrapper.Initialize();
            var count = Trace.Listeners.Count;
            Bootstrapper.Initialize();
            Trace.Listeners.Count.Should().Be(count);
        }
    }
}