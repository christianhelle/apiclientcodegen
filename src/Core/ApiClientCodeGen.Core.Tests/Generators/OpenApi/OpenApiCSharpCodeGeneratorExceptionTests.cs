using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.OpenApi
{
    public class OpenApiCSharpCodeGeneratorExceptionTests
    {
        [Theory, AutoMoqData]
        public void Constructor_Requires_SwaggerFile(IProcessLauncher launcher, IDependencyInstaller installer)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        null,
                        null,
                        null,
                        launcher,
                        installer))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Requires_DefaultNamespace(IProcessLauncher launcher, IDependencyInstaller installer)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        "",
                        null,
                        null,
                        launcher,
                        installer))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Requires_Options(IProcessLauncher launcher, IDependencyInstaller installer)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        "",
                        "",
                        null,
                        launcher,
                        installer))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Requires_ProcessLauncher(IDependencyInstaller installer)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        "",
                        "",
                        null,
                        null,
                        installer))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Requires_DependencyInstaller(IProcessLauncher launcher)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        "",
                        "",
                        null,
                        launcher,
                        null))
                .Should()
                .ThrowExactly<ArgumentNullException>();
    }
}