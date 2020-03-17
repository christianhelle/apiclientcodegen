using System;
using System.ComponentModel;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators
{
    [TestClass]
    public class ProcessLauncherTests
    {
        [TestMethod, Xunit.Fact]
        public void Start_Random_Command_Throws_Win32Exception()
        {
            new Action(
                    () => new ProcessLauncher()
                        .Start(
                            Test.CreateAnnonymous<string>(),
                            Test.CreateAnnonymous<string>()))
                .Should()
                .ThrowExactly<Win32Exception>();
        }
        
        [TestMethod, Xunit.Fact]
        public void Start_Invalid_Throws_InvalidOperationException()
        {
            new Action(
                    () => new ProcessLauncher()
                        .Start(
                            "java",
                            Test.CreateAnnonymous<string>()))
                .Should()
                .ThrowExactly<InvalidOperationException>();
        }
    }
}