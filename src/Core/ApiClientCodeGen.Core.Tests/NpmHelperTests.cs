using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests
{
    public class NpmHelperTests
    {
        [Fact]
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