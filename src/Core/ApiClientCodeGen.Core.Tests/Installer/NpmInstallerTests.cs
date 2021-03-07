using System;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Installer
{
    public class NpmInstallerTests
    {
        [Fact]
        public void Implements_Interface()
            => typeof(NpmInstaller)
                .Should()
                .Implement<INpmInstaller>();

        [Fact]
        public void Requires_ProcessLauncher()
            => new Action(() => new NpmInstaller(null))
                .Should()
                .ThrowExactly<ArgumentNullException>();


        [Theory, AutoMoqData]
        public async Task InstallNpmPackage_Invokes_Process_Start(
            [Frozen] IProcessLauncher processLauncher,
            NpmInstaller sut,
            string packageName)
        {
            await sut.InstallNpmPackage(packageName);

            Mock.Get(processLauncher)
                .Verify(
                    c => c.Start(
                        It.IsAny<string>(),
                        $"install -g {packageName} --force",
                        null));
        }
    }
}