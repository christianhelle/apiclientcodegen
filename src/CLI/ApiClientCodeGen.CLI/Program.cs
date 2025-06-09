using System;
using System.Diagnostics.CodeAnalysis;
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
using Microsoft.Extensions.Logging;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core.Options.Refitter;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Rapicgen.CLI
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var verboseOptions = new VerboseOption(args);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IVerboseOptions>(verboseOptions);
            ConfigureServices(serviceCollection);
            
            if (verboseOptions.Enabled)
                serviceCollection.AddLogging(b => b.AddConsole());

            Logger.Setup().WithDefaultTags("CLI");

            var app = new CommandApp(new TypeRegistrar(serviceCollection));
            
            app.Configure(config =>
            {
                config.AddBranch<BaseCommandSettings>("csharp", csharp =>
                {
                    csharp.SetDescription("Generate C# API clients");
                    csharp.AddCommand<AutoRestCommand>("autorest")
                        .WithDescription("AutoRest (v3.0.0-beta.20210504.2)");
                    csharp.AddCommand<RefitterCommandSpectre>("refitter")
                        .WithDescription("Refitter (v1.5.5)");
                    // More commands will be added as they are migrated
                });
                
                // More top-level commands will be added as they are migrated
            });

            try
            {
                return await app.RunAsync(args);
            }
            catch (Exception ex)
            {
                Logger.Instance.TrackError(new CommandLineException(ex.Message, ex));
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                return ResultCodes.Error;
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(b => b.AddDebug());
            services.AddSingleton<IAnsiConsole>(AnsiConsole.Console);
            services.AddSingleton<IConsoleOutput, ConsoleOutput>();
            services.AddSingleton<IGeneralOptions, DefaultGeneralOptions>();
            services.AddSingleton<IAutoRestOptions, DefaultAutoRestOptions>();
            services.AddSingleton<IOpenApiGeneratorOptions, DefaultOpenApiGeneratorOptions>();
            services.AddSingleton<IRefitterOptions, DefaultRefitterOptions>();
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
            services.AddSingleton<IRefitterCodeGeneratorFactory, RefitterCodeGeneratorFactory>();
            services.AddSingleton<IDependencyInstaller, DependencyInstaller>();
            services.AddSingleton<INpmInstaller, NpmInstaller>();
            services.AddSingleton<IFileDownloader, FileDownloader>();
            services.AddSingleton<IWebDownloader, WebDownloader>();
        }
    }
}
