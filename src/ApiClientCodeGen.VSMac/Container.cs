using System;
using System.Diagnostics.CodeAnalysis;
using ApiClientCodeGen.VSMac.Commands.NSwagStudio;
using ApiClientCodeGen.VSMac.CustomTools;
using ApiClientCodeGen.VSMac.CustomTools.AutoRest;
using ApiClientCodeGen.VSMac.CustomTools.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using Microsoft.Extensions.DependencyInjection;

namespace ApiClientCodeGen.VSMac
{
    public class Container
    {
        private readonly IServiceProvider serviceProvider;

        private static readonly Lazy<Container> LazyInstance = new Lazy<Container>();
        public static Container Instance => LazyInstance.Value;

        [ExcludeFromCodeCoverage]
        public Container()
        {
            var services = new ServiceCollection();

            if (TestingUtility.IsRunningFromUnitTest)
                services.AddSingleton<ILoggingService, DefaultLoggingService>();
            else
                services.AddSingleton<ILoggingService, MonoDevelopLoggingService>();

            services.AddSingleton<LoggingServiceTraceListener>();

            services.AddSingleton<IGeneralOptions, DefaultGeneralOptions>();
            services.AddSingleton<INSwagOptions, DefaultNSwagOptions>();
            services.AddSingleton<INSwagStudioOptions, DefaultNSwagStudioOptions>();
            services.AddSingleton<IProcessLauncher, ProcessLauncher>();

            services.AddSingleton<INSwagStudioCodeGeneratorFactory, NSwagStudioCodeGeneratorFactory>();
            services.AddSingleton<GenerateNSwagStudioCommand>();

            services.AddSingleton<IOpenApiDocumentFactory, OpenApiDocumentFactory>();
            services.AddSingleton<INSwagCodeGeneratorFactory, NSwagCodeGeneratorFactory>();
            services.AddSingleton<NSwagCSharpCodeGenerator>();

            services.AddSingleton<IAutoRestCodeGeneratorFactory, AutoRestCodeGeneratorFactory>();
            services.AddSingleton<IAutoRestOptions, DefaultAutoRestOptions>();

            serviceProvider = services.BuildServiceProvider();
        }

        public T Resolve<T>() where T : class
            => serviceProvider.GetService(typeof(T)) as T;
    }
}