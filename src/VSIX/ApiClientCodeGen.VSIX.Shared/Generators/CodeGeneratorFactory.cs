﻿using System;
using Rapicgen.Core;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.AutoRest;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options;
using Rapicgen.Core.Options.AutoRest;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.OpenApiGenerator;
using Rapicgen.Options;
using Rapicgen.Options.AutoRest;
using Rapicgen.Options.General;
using Rapicgen.Options.NSwag;
using Rapicgen.Options.OpenApiGenerator;
using OpenApiDocumentFactory =
    Rapicgen.Generators.NSwag.OpenApiDocumentFactory;

namespace Rapicgen.Generators
{
    public class CodeGeneratorFactory : ICodeGeneratorFactory
    {
        private readonly IDependencyInstaller dependencyInstaller;
        private readonly IRemoteLogger remoteLogger;
        private readonly IOpenApiDocumentFactory documentFactory;
        private readonly IProcessLauncher processLauncher;
        private readonly IOptionsFactory optionsFactory;

        public CodeGeneratorFactory(
            IOptionsFactory? optionsFactory = null,
            IProcessLauncher? processLauncher = null,
            IOpenApiDocumentFactory? documentFactory = null,
            IRemoteLogger? remoteLogger = null,
            IDependencyInstaller? dependencyInstaller = null)
        {
            this.remoteLogger = remoteLogger ?? Logger.Instance;
            this.optionsFactory = optionsFactory ?? new OptionsFactory();
            this.processLauncher = processLauncher ?? new ProcessLauncher();
            this.documentFactory = documentFactory ?? new OpenApiDocumentFactory();
            this.dependencyInstaller = dependencyInstaller ??
                                       new DependencyInstaller(
                                           new NpmInstaller(this.processLauncher),
                                           new FileDownloader(new WebDownloader()));
        }

        public ICodeGenerator Create(
            string defaultNamespace,
            string inputFileContents,
            string inputFilePath,
            SupportedLanguage language,
            SupportedCodeGenerator generator)
        {
            remoteLogger.TrackFeatureUsage(generator.GetName());

            switch (generator)
            {
                case SupportedCodeGenerator.AutoRest:
                    return new AutoRestCSharpCodeGenerator(
                        inputFilePath,
                        defaultNamespace,
                        optionsFactory.Create<IAutoRestOptions, AutoRestOptionsPage>(),
                        processLauncher,
                        documentFactory,
                        dependencyInstaller);

                case SupportedCodeGenerator.NSwag:
                    return new NSwagCSharpCodeGenerator(
                        inputFilePath,
                        new OpenApiDocumentFactory(),
                        new NSwagCodeGeneratorSettingsFactory(
                            defaultNamespace,
                            optionsFactory.Create<INSwagOptions, NSwagOptionsPage>()));

                case SupportedCodeGenerator.Swagger:
                    return new SwaggerCSharpCodeGenerator(
                        inputFilePath,
                        defaultNamespace,
                        optionsFactory.Create<IGeneralOptions, GeneralOptionPage>(),
                        processLauncher,
                        dependencyInstaller);

                case SupportedCodeGenerator.OpenApi:
                    return new OpenApiCSharpCodeGenerator(
                        inputFilePath,
                        defaultNamespace,
                        optionsFactory.Create<IGeneralOptions, GeneralOptionPage>(),
                        optionsFactory.Create<IOpenApiGeneratorOptions, OpenApiGeneratorOptionsPage>(),
                        processLauncher,
                        dependencyInstaller);

                default:
                    throw new NotSupportedException();
            }
        }
    }
}