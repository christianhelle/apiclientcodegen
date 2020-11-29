using System;
using ApiClientCodeGen.CLI.Tests.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests
{
    public class NpmHelperTests
    {
        [Theory, AutoMoqData]
        public void TryGetNpmPrefixPathFromNpmConfig_Returns_NotNull()
        {
            NpmHelper.TryGetNpmPrefixPathFromNpmConfig()
                .Should()
                .NotBeNull();
        }
        
        [Theory, AutoMoqData]
        public void TryGetNpmPrefixPathFromNpmConfig_Returns_Null_Upon_Exception(IProcessLauncher process)
        {
            Mock.Get(process)
                .Setup(
                    c => c.Start(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        It.IsAny<string>()))
                .Throws<Exception>();
            NpmHelper.TryGetNpmPrefixPathFromNpmConfig(process)
                .Should()
                .BeNull();
        }
    }
}