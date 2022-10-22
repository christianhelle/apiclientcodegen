﻿using System.IO;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Moq;

namespace ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3
{
    public class SwaggerCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IGeneralOptions> OptionsMock = new Mock<IGeneralOptions>();
        public string Code;

        protected override void OnInitialize()
        {
            ThrowNotSupportedOnUnix();
            
            OptionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetJavaPath());
            
            var codeGenerator = new SwaggerCSharpCodeGenerator(
                Path.GetFullPath(SwaggerV3JsonFilename),
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