using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.OpenApiGenerator;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.OpenApi
{
    public class OpenApiCSharpCodeGeneratorExceptionTests
    {
        [Theory, AutoMoqData]
        public void Constructor_Requires_SwaggerFile(
            IProcessLauncher launcher,
            IDependencyInstaller installer,
            IOpenApiGeneratorOptions openApiGeneratorOptions)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        null,
                        null,
                        null,
                        openApiGeneratorOptions,
                        launcher,
                        installer))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Requires_DefaultNamespace(
            IProcessLauncher launcher,
            IDependencyInstaller installer,
            IOpenApiGeneratorOptions openApiGeneratorOptions)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        "",
                        null,
                        null,
                        openApiGeneratorOptions,
                        launcher,
                        installer))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Requires_Options(
            IProcessLauncher launcher,
            IDependencyInstaller installer,
            IOpenApiGeneratorOptions openApiGeneratorOptions)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        "",
                        "",
                        null,
                        openApiGeneratorOptions,
                        launcher,
                        installer))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Requires_ProcessLauncher(
            IDependencyInstaller installer,
            IOpenApiGeneratorOptions openApiGeneratorOptions)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        "",
                        "",
                        null,
                        openApiGeneratorOptions,
                        null,
                        installer))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Requires_DependencyInstaller(
            IProcessLauncher launcher,
            IOpenApiGeneratorOptions openApiGeneratorOptions)
            => new Action(
                    () => new OpenApiCSharpCodeGenerator(
                        "",
                        "",
                        null,
                        openApiGeneratorOptions,
                        launcher,
                        null))
                .Should()
                .ThrowExactly<ArgumentNullException>();
    }
}