using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Installer;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.Swagger
{
    public class SwaggerCSharpCodeGeneratorExceptionTests
    {
        [Theory, AutoMoqData]
        public void Constructor_Requires_SwaggerFile(IProcessLauncher launcher, IDependencyInstaller installer)
            => new Action(
                    () => new SwaggerCSharpCodeGenerator(
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
                    () => new SwaggerCSharpCodeGenerator(
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
                    () => new SwaggerCSharpCodeGenerator(
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
                    () => new SwaggerCSharpCodeGenerator(
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
                    () => new SwaggerCSharpCodeGenerator(
                        "",
                        "",
                        null,
                        launcher,
                        null))
                .Should()
                .ThrowExactly<ArgumentNullException>();
    }
}