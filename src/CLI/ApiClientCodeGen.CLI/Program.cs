using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using Rapicgen.CLI.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Exceptions;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.AutoRest;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.OpenApiGenerator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rapicgen.CLI.Commands.CSharp;

namespace Rapicgen.CLI
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

            Logger.Setup().WithDefaultTags("CLI");

            try
            {
                return await builder.RunCommandLineApplicationAsync<RootCommand>(args);
            }
            catch (TargetInvocationException ex) when (ex.InnerException != null)
            {
                Logger.Instance.TrackError(new CommandLineException(ex.Message, ex));
                Console.WriteLine($@"Error: {ex.InnerException.Message}");
                return ResultCodes.Error;
            }
            catch (Exception ex)
            {
                Logger.Instance.TrackError(new CommandLineException(ex.Message, ex));
                Console.WriteLine($@"Error: {ex.Message}");
                return ResultCodes.Error;
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(b => b.AddDebug());
            services.AddSingleton<IConsoleOutput, ConsoleOutput>();
            services.AddSingleton<IGeneralOptions, DefaultGeneralOptions>();
            services.AddSingleton<IAutoRestOptions, DefaultAutoRestOptions>();
            services.AddSingleton<IOpenApiGeneratorOptions, DefaultOpenApiGeneratorOptions>();
            services.AddSingleton<IProgressReporter, ProgressReporter>();
            services.AddSingleton<IOpenApiDocumentFactory, OpenApiDocumentFactory>();
            services.AddSingleton<INSwagCodeGeneratorSettingsFactory, NSwagCodeGeneratorSettingsFactory>();
            services.AddSingleton<IProcessLauncher, ProcessLauncher>();
            services.AddSingleton<IOpenApiGeneratorFactory, OpenApiGeneratorFactory>();
            services.AddSingleton<IJMeterCodeGeneratorFactory, JMeterCodeGeneratorFactory>();
            services.AddSingleton<ITypeScriptCodeGeneratorFactory, TypeScriptCodeGeneratorFactory>();
            services.AddSingleton<IAutoRestCodeGeneratorFactory, AutoRestCodeGeneratorFactory>();
            services.AddSingleton<INSwagCodeGeneratorFactory, NSwagCodeGeneratorFactory>();
            services.AddSingleton<IOpenApiCSharpGeneratorFactory, OpenApiCSharpGeneratorFactory>();
            services.AddSingleton<ISwaggerCodegenFactory, SwaggerCodegenFactory>();
            services.AddSingleton<IDependencyInstaller, DependencyInstaller>();
            services.AddSingleton<INpmInstaller, NpmInstaller>();
            services.AddSingleton<IFileDownloader, FileDownloader>();
            services.AddSingleton<IWebDownloader, WebDownloader>();
        }
    }
}
