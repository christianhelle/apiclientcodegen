using System;
using System.ComponentModel;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;


namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators
{
    
    public class ProcessLauncherTests
    {
        [Xunit.Fact]
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
        
        [Xunit.Fact]
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