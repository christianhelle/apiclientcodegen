using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.CLI.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using FluentAssertions;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class TypeScriptCodeGeneratorFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            TypeScriptCodeGeneratorFactory sut,
            OpenApiTypeScriptGenerator generator,
            string swaggerFile,
            string oututPath,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => sut.Create(
                    generator,
                    swaggerFile,
                    oututPath,
                    options,
                    processLauncher,
                    dependencyInstaller)
                .Should()
                .NotBeNull();
    }
}