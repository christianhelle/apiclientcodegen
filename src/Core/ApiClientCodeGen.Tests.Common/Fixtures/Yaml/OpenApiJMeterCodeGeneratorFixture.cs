﻿using System.IO;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Moq;

namespace ApiClientCodeGen.Tests.Common.Fixtures.Yaml
{
    public class OpenApiJMeterCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IGeneralOptions> OptionsMock = new Mock<IGeneralOptions>();
        public string Code;

        protected override void OnInitialize()
        {
            ThrowNotSupportedOnUnix();
            
            OptionsMock.Setup(c => c.JavaPath).Returns(PathProvider.GetJavaPath());

            var codeGenerator = new OpenApiJMeterCodeGenerator(
                Path.GetFullPath(SwaggerYamlFilename),
                "GeneratedCode",
                OptionsMock.Object,
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader())));

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}