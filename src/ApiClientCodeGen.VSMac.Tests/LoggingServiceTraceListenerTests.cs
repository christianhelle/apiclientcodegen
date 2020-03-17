using System;
using System.Diagnostics;
using ApiClientCodeGen.VSMac.Logging;
using ApiClientCodeGen.VSMac.Tests.Infrastructure;
using FluentAssertions;
using Moq;
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

        [Fact]
        public void Requires_ILoggingService()
            => new Action(() => new LoggingServiceTraceListener(null))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Can_Write(LoggingServiceTraceListener sut, string message)
            => new Action(() => sut.Write(message))
                .Should()
                .NotThrow();

        [Theory, AutoMoqData]
        public void Can_WriteLine(LoggingServiceTraceListener sut, string message)
            => new Action(() => sut.WriteLine(message))
                .Should()
                .NotThrow();

        [Theory, AutoMoqData]
        public void Write_Invokes_Log(ILoggingService logger, string message)
        {
            var sut = new LoggingServiceTraceListener(logger);
            sut.Write(message);
            Mock.Get(logger).Verify(c => c.Log(message));
        }

        [Theory, AutoMoqData]
        public void WriteLine_Invokes_Log(ILoggingService logger, string message)
        {
            var sut = new LoggingServiceTraceListener(logger);
            sut.WriteLine(message);
            Mock.Get(logger).Verify(c => c.Log(message));
        }
    }
}