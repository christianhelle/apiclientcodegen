using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Moq;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.Kiota;

public class KiotaCodeGeneratorTests
{
    [Theory, AutoMoqData]
    public void GenerateCode_WhenCalled_CallsDependencyInstallerInstallKiota(
        [Frozen] IDependencyInstaller dependencyInstaller,
        KiotaCodeGenerator sut)
    {
        sut.GenerateCode(null);
        Mock.Get(dependencyInstaller)
            .Verify(x => x.InstallKiota(), Times.Once);
    }

    [Theory, AutoMoqData]
    public void GenerateCode_WhenCalled_CallsProcessLauncher(
        [Frozen] IProcessLauncher processLauncher,
        KiotaCodeGenerator sut)
    {
        sut.GenerateCode(null);
        Mock.Get(processLauncher)
            .Verify(
                x => x.Start("kiota", It.IsAny<string>(), null),
                Times.Once);
    }

    [Theory, AutoMoqData]
    public void GenerateCode_WhenCalled_Reports_Progress(
        [Frozen] IProgressReporter progressReporter,
        KiotaCodeGenerator sut)
    {
        sut.GenerateCode(progressReporter);
        Mock.Get(progressReporter)
            .Verify(
                x => x.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeast(4));
    }
}