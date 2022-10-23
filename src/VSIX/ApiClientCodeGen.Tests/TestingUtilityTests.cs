using Rapicgen.Core;
using FluentAssertions;

namespace Rapicgen.Tests
{
       
    public class TestingUtilityTests
    {
        [Xunit.Fact]
        public void IsRunningFromUnitTest_BeTrue()
            => TestingUtility.IsRunningFromUnitTest.Should().BeTrue();
    }
}