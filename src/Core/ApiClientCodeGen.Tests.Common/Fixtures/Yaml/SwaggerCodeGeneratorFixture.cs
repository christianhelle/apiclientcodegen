﻿using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using Moq;

namespace ApiClientCodeGen.Tests.Common.Fixtures.Yaml
{
    public class SwaggerCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IGeneralOptions> OptionsMock = new Mock<IGeneralOptions>();
        public string Code;

        protected override void OnInitialize()
        {
            ThrowNotSupportedOnUnix();
            
            OptionsMock.Setup(c => c.JavaPath).Returns(PathProvider.GetJavaPath());
            
            var codeGenerator = new SwaggerCSharpCodeGenerator(
                Path.GetFullPath(SwaggerYamlFilename),
                "GeneratedCode",
                OptionsMock.Object,
                new ProcessLauncher());

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}