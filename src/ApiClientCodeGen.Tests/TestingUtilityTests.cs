using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;


namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
       
    public class TestingUtilityTests
    {
        [Xunit.Fact]
        public void IsRunningFromUnitTest_BeTrue()
            => TestingUtility.IsRunningFromUnitTest.Should().BeTrue();
    }
}