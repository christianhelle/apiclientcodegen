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
using Rapicgen.Core.Options.OpenApiGenerator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core.Options.Refitter;
using Spectre.Console.Cli;

namespace Rapicgen.CLI
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        public static int Main(string[] args)
        {
            Logger.Setup().WithDefaultTags("CLI");

            try
            {
                var services = new ServiceCollection();
                ConfigureServices(services);

                var app = new CommandApp(new TypeRegistrar(services));
                app.Configure(config =>
                {
                    config.CaseSensitivity(CaseSensitivity.None);
                    config.SetApplicationName("rapicgen");
                    config.AddBranch("csharp", cs =>
                    {
                        cs.SetDescription("Generate C# API clients using various generators");

                        // Individual commands (no subcommands structure for now)
                        cs.AddCommand<AutoRestCommand>("autorest")
                            .WithDescription("Generate C# code using AutoRest")
                            .WithExample(new[] { "autorest", "petstore.json", "GeneratedCode", "Output.cs" });

                        cs.AddCommand<KiotaCommand>("kiota")
                            .WithDescription("Generate C# code using Microsoft Kiota")
                            .WithExample(new[] { "kiota", "petstore.json", "GeneratedCode", "Output.cs" });

                        cs.AddCommand<NSwagCommand>("nswag")
                            .WithDescription("Generate C# code using NSwag")
                            .WithExample(new[] { "nswag", "petstore.json", "GeneratedCode", "Output.cs" });

                        cs.AddCommand<RefitterCommand>("refitter")
                            .WithDescription("Generate C# code using Refitter")
                            .WithExample(new[] { "refitter", "petstore.json", "GeneratedCode", "Output.cs" });

                        cs.AddCommand<SwaggerCodegenCommand>("swagger")
                            .WithDescription("Generate C# code using Swagger Codegen")
                            .WithExample(new[] { "swagger", "petstore.json", "GeneratedCode", "Output.cs" });

                        cs.AddCommand<OpenApiCSharpGeneratorCommand>("openapi")
                            .WithDescription("Generate C# code using OpenAPI Generator")
                            .WithExample(new[] { "openapi", "petstore.json", "GeneratedCode", "Output.cs" });
                    });

                    config.AddCommand<TypeScriptCommand>("typescript")
                        .WithDescription("Generate TypeScript API clients")
                        .WithExample(new[] { "typescript", "petstore.json", "typescript-generated-code" });

                    config.AddCommand<JMeterCommand>("jmeter")
                        .WithDescription("Generate Apache JMeter test plans")
                        .WithExample(new[] { "jmeter", "petstore.json", "JMeter" });

                    config.AddCommand<OpenApiGeneratorCommand>("openapi-generator")
                        .WithDescription("Generate code using OpenAPI Generator")
                        .WithExample(new[] { "openapi-generator", "csharp", "petstore.json", "GeneratedCode" });
                });

                return app.Run(args);
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