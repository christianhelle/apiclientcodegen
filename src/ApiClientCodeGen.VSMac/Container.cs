using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
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
        
        public Container()
        {
            var services = new ServiceCollection();
            services.AddSingleton<LoggingServiceTraceListener>();
            services.AddSingleton<INSwagOptions, DefaultNSwagOptions>();
            services.AddSingleton<INSwagStudioOptions, DefaultNSwagStudioOptions>();
            services.AddTransient<IProcessLauncher, ProcessLauncher>();

            serviceProvider = services.BuildServiceProvider();
        }

        public T Resolve<T>() where T : class
            => serviceProvider.GetService(typeof(T)) as T;
    }
}