using System;
using System.Reflection;
using System.Threading.Tasks;
using ApiClientCodeGen.CLI.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiClientCodeGen.CLI
{
    internal static class Program
    {
        static async Task<int> Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices(ConfigureServices);

            if (VerboseOption.Parse(args))
                builder.ConfigureLogging(b => b.AddConsole());

            try
            {
                return await builder
                    .RunCommandLineApplicationAsync<RootCommand>(
                        args);
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

        private static void ConfigureServices(HostBuilderContext arg1, IServiceCollection arg2)
        {
            // Method intentionally left empty.
        }
    }
}
