using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.Refitter;
using FluentAssertions;
using Rapicgen.CLI.Commands.CSharp;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class RefitterCodeGeneratorFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            RefitterCodeGeneratorFactory sut,
            string swaggerFile,
            string defaultNamespace,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller,
            IRefitterOptions options)
            => sut.Create(
                    swaggerFile,
                    defaultNamespace,
                    processLauncher,
                    dependencyInstaller,
                    options)
                .Should()
                .NotBeNull();
    }
}