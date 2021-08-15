using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.OpenApiGenerator;

namespace ApiClientCodeGen.VSMac.CustomTools.OpenApi
{
    public class OpenApiSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "OpenApiCodeGenerator";

        private readonly IGeneralOptions options;
        private readonly IOpenApiGeneratorOptions openApiGeneratorOptions;
        private readonly IProcessLauncher processLauncher;
        private readonly IOpenApiGeneratorFactory factory;
        private readonly IDependencyInstaller dependencyInstaller;

        public OpenApiSingleFileCustomTool()
            : this(
                Container.Instance.Resolve<IGeneralOptions>(),
                Container.Instance.Resolve<IOpenApiGeneratorOptions>(),
                Container.Instance.Resolve<IProcessLauncher>(),
                Container.Instance.Resolve<IOpenApiGeneratorFactory>(),
                Container.Instance.Resolve<IDependencyInstaller>())
        {
        }

        public OpenApiSingleFileCustomTool(
            IGeneralOptions options,
            IOpenApiGeneratorOptions openApiGeneratorOptions,
            IProcessLauncher processLauncher,
            IOpenApiGeneratorFactory factory,
            IDependencyInstaller dependencyInstaller)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.openApiGeneratorOptions = openApiGeneratorOptions ?? throw new ArgumentNullException(nameof(openApiGeneratorOptions));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace)
            => factory.Create(
                swaggerFile,
                customToolNamespace,
                options,
                openApiGeneratorOptions,
                processLauncher,
                dependencyInstaller);
    }
}