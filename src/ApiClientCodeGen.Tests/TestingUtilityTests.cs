using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]   
    public class TestingUtilityTests
    {
        [TestMethod, Xunit.Fact]
        public void IsRunningFromUnitTest_BeTrue()
            => TestingUtility.IsRunningFromUnitTest.Should().BeTrue();
    }
}