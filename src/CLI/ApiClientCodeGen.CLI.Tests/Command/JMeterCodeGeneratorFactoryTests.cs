using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.CLI.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using FluentAssertions;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class JMeterCodeGeneratorFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            JMeterCodeGeneratorFactory sut,
            string swaggerFile,
            string oututPath,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => sut.Create(
                    swaggerFile,
                    oututPath,
                    options,
                    processLauncher,
                    dependencyInstaller)
                .Should()
                .NotBeNull();
    }
}