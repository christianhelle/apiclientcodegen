using System;
using System.ComponentModel;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;

namespace ApiClientCodeGen.Core.Tests.Generators
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
        public void Start_Invalid_Throws_ProcessLaunchException()
        {
            new Action(
                    () => new ProcessLauncher()
                        .Start(
                            "java",
                            Test.CreateAnnonymous<string>()))
                .Should()
                .ThrowExactly<ProcessLaunchException>();
        }
    }
}