using System;
using ApiClientCodeGen.CLI.Commands;
using ApiClientCodeGen.CLI.Tests.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests.Command
{
    public class CodeGeneratorCommandFactoryTests
    {
        [Theory, AutoMoqData]
        public void CreateAutoRestCommand_Should_Return_NotNull(
            AutoRestCodeGeneratorFactory sut,
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher)
            => sut.Create(
                    swaggerFile,
                    defaultNamespace,
                    options,
                    processLauncher)
                .Should()
                .NotBeNull();
    }
}
