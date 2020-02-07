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
        public void Create_AutoRestCommand_Should_Return_NotNull(
            CodeGeneratorCommandFactory sut,
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher)
            => sut.Create<AutoRestCommand>(
                    swaggerFile,
                    defaultNamespace,
                    options,
                    processLauncher)
                .Should()
                .NotBeNull();

        [Theory, AutoMoqData]
        public void Create_CodeGeneratorCommand_Should_Throw_NotSupported(
            CodeGeneratorCommandFactory sut,
            string swaggerFile,
            string defaultNamespace,
            object options,
            IProcessLauncher processLauncher)
            => new Action(
                    () => sut.Create<CodeGeneratorCommand>(
                        swaggerFile,
                        defaultNamespace,
                        options,
                        processLauncher))
                .Should()
                .ThrowExactly<NotSupportedException>();
    }
}
