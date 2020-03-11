using System;
using System.Diagnostics;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.VSMac.Tests
{
    public class LoggingServiceTraceListenerTests
    {
        [Fact]
        public void Extends_TraceListener()
            => typeof(LoggingServiceTraceListener)
                .Should()
                .BeAssignableTo<TraceListener>();

        [Theory, AutoData]
        public void Can_Write(string message)
            => new Action(() => new LoggingServiceTraceListener().Write(message))
                .Should()
                .NotThrow();

        [Theory, AutoData]
        public void Can_WriteLine(string message)
            => new Action(() => new LoggingServiceTraceListener().WriteLine(message))
                .Should()
                .NotThrow();
    }
}