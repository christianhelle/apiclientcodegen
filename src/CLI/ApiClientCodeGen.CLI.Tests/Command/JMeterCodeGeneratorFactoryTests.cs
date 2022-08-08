using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Tests.Command
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
            IJMeterCodeGeneratorFactory factory,
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