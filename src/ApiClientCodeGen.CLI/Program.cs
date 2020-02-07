using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ApiClientCodeGen.CLI.Commands;
using ApiClientCodeGen.CLI.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiClientCodeGen.CLI
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        public static int Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices(ConfigureServices);

            if (VerboseOption.Parse(args))
                builder.ConfigureLogging(b => b.AddConsole());

            try
            {
                return builder
                    .RunCommandLineApplicationAsync<RootCommand>(args)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (TargetInvocationException ex) when (ex.InnerException != null)
            {
                Console.WriteLine($@"Error: {ex.InnerException.Message}");
                return ResultCodes.Error;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error: {ex.Message}");
                return ResultCodes.Error;
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(b => b.AddDebug());
            services.AddTransient<INSwagOptions, NSwagOptions>();
            services.AddTransient<IGeneralOptions, GeneralOptions>();
            services.AddTransient<IAutoRestOptions, AutoRestOptions>();
            services.AddTransient<IProgressReporter, ProgressReporter>();
            services.AddTransient<IOpenApiDocumentFactory, OpenApiDocumentFactory>();
            services.AddTransient<INSwagCodeGeneratorSettingsFactory, NSwagCodeGeneratorSettingsFactory>();
            services.AddTransient<IProcessLauncher, ProcessLauncher>();
            services.AddTransient<IConsoleOutput, ConsoleOutput>();
            services.AddTransient<IAutoRestCodeGeneratorFactory, AutoRestCodeGeneratorFactory>();
            services.AddTransient<INSwagCodeGeneratorFactory, NSwagCodeGeneratorFactory>();
            services.AddTransient<IOpenApiGeneratorFactory, OpenApiGeneratorFactory>();
            services.AddTransient<ISwaggerCodegenFactory, SwaggerCodegenFactory>();
        }
    }
}
