using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.Core.Logging;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Logging
{
    public class ConsoleOutputTraceListenerTests
    {
        [Theory, AutoMoqData]
        public void Console_NotNull(ConsoleOutputTraceListener sut)
            => sut.Console.Should().NotBeNull();
        
        [Theory, AutoMoqData]
        public void Write_Invokes_ConsoleOutput_WriteLine(ConsoleOutputTraceListener sut, string text)
        {
            sut.Write(text);
            Mock.Get(sut.Console).Verify(c => c.WriteLine(text));
        }
        
        [Theory, AutoMoqData]
        public void WriteLine_Invokes_ConsoleOutput_WriteLine(ConsoleOutputTraceListener sut, string text)
        {
            sut.WriteLine(text);
            Mock.Get(sut.Console).Verify(c => c.WriteLine(text));
        }
    }
}