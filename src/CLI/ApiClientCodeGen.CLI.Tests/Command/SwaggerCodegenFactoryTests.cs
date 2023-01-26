using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.CLI.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using FluentAssertions;
using Rapicgen.CLI.Commands.CSharp;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class SwaggerCodegenFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            SwaggerCodegenFactory sut,
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => sut.Create(
                    swaggerFile,
                    defaultNamespace,
                    options,
                    processLauncher,
                    dependencyInstaller)
                .Should()
                .NotBeNull();
    }
}