using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Commands;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
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
        public static async Task<int> Main(string[] args)
        {
            var verboseOptions = new VerboseOption(args);

            var builder = new HostBuilder()
                .ConfigureServices(s => s.AddSingleton<IVerboseOptions>(verboseOptions))
                .ConfigureServices(ConfigureServices);
            
            if (verboseOptions.Enabled)
                builder.ConfigureLogging(b => b.AddConsole());

            try
            {
                return await builder.RunCommandLineApplicationAsync<RootCommand>(args);
            }
            catch (TargetInvocationException ex) when (ex.InnerException != null)
            {
                Logger.Instance.TrackError(ex);
                Console.WriteLine($@"Error: {ex.InnerException.Message}");
                return ResultCodes.Error;
            }
            catch (Exception ex)
            {
                Logger.Instance.TrackError(ex);
                Console.WriteLine($@"Error: {ex.Message}");
                return ResultCodes.Error;
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(b => b.AddDebug());
            services.AddSingleton<IConsoleOutput, ConsoleOutput>();
            services.AddSingleton<INSwagOptions, DefaultNSwagOptions>();
            services.AddSingleton<IGeneralOptions, DefaultGeneralOptions>();
            services.AddSingleton<IAutoRestOptions, DefaultAutoRestOptions>();
            services.AddSingleton<IProgressReporter, ProgressReporter>();
            services.AddSingleton<IOpenApiDocumentFactory, OpenApiDocumentFactory>();
            services.AddSingleton<INSwagCodeGeneratorSettingsFactory, NSwagCodeGeneratorSettingsFactory>();
            services.AddSingleton<IProcessLauncher, ProcessLauncher>();
            services.AddSingleton<IAutoRestCodeGeneratorFactory, AutoRestCodeGeneratorFactory>();
            services.AddSingleton<INSwagCodeGeneratorFactory, NSwagCodeGeneratorFactory>();
            services.AddSingleton<IOpenApiGeneratorFactory, OpenApiGeneratorFactory>();
            services.AddSingleton<ISwaggerCodegenFactory, SwaggerCodegenFactory>();
        }
    }
}
