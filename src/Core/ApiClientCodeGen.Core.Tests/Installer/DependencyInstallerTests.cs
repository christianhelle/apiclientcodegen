using System;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Installer
{
    public class DependencyInstallerTests
    {
        [Fact]
        public void Implements_Interface()
            => typeof(DependencyInstaller)
                .Should()
                .Implement<IDependencyInstaller>();

        [Fact]
        public void Requires_INpmInstaller()
            => new Action(() => new DependencyInstaller(null))
                .Should()
                .Throw<ArgumentNullException>();

        [Theory, AutoMoqData]
        public async Task InstallAutoRest_Invokes_Npm(
            [Frozen] INpmInstaller npm,
            DependencyInstaller sut)
        {
            await sut.InstallAutoRest();
            Mock.Get(npm)
                .Verify(c => c.InstallNpmPackage("autorest"));
        }

        [Theory, AutoMoqData]
        public async Task InstallNSwag_Invokes_Npm(
            [Frozen] INpmInstaller npm,
            DependencyInstaller sut)
        {
            await sut.InstallNSwag();
            Mock.Get(npm)
                .Verify(c => c.InstallNpmPackage("nswag"));
        }
    }
}