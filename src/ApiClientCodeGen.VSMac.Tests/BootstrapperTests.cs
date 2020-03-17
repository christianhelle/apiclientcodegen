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
                .Should()
                .Contain(
                    Container.Instance.Resolve<LoggingServiceTraceListener>());
        }

        [Fact]
        public void Initialize_Twice_Does_Nothing()
        {
            Bootstrapper.Initialize();
            Bootstrapper.Initialize();
            Trace.Listeners.Should().OnlyHaveUniqueItems();
        }
    }
}