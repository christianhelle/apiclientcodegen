using System;
using System.ComponentModel;
using Rapicgen.Core.External;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.Installer
{
    public class DependencyInstaller : IDependencyInstaller
    {
        private readonly INpmInstaller npm;
        private readonly IFileDownloader downloader;
        private readonly IProcessLauncher processLauncher;

        public DependencyInstaller(
            INpmInstaller npm,
            IFileDownloader downloader,
            IProcessLauncher processLauncher)
        {
            this.npm = npm ?? throw new ArgumentNullException(nameof(npm));
            this.downloader = downloader ?? throw new ArgumentNullException(nameof(downloader));
            this.processLauncher = processLauncher;
        }

        public void InstallAutoRest()
        {
            npm.InstallNpmPackage("autorest");
        }

        public void InstallNSwag()
        {
            npm.InstallNpmPackage("nswag");
        }

        public string InstallOpenApiGenerator()
        {
            return downloader.DownloadFile(
                "openapi-generator-cli.jar",
                Resource.OpenApiGenerator_SHA1,
                Resource.OpenApiGenerator_DownloadUrl);
        }

        public string InstallSwaggerCodegen()
        {
            return downloader.DownloadFile(
                "swagger-codegen-cli.jar",
                Resource.SwaggerCodegenCli_SHA1,
                Resource.SwaggerCodegenCli_DownloadUrl);
        }

        public void InstallKiota()
        {
            var command = "kiota";
            string arguments = "--version";
            string kiotaVersion = "";
            try
            {
                processLauncher.Start(command, arguments, output =>
                {
                    if (output != null)
                    {
                        kiotaVersion = output ?? kiotaVersion;

                        Logger.Instance.WriteLine(output);
                    }
                }, error =>
                {
                    if (error != null)
                    {
                        Logger.Instance.WriteLine(error);
                    }
                });
                if (!kiotaVersion.StartsWith("1.22.0"))
                { 
                    //older or newer? i guess this should be handled.
                }
            }
            catch(Win32Exception e)
            {
                //if command doesn't exist Win32Exception is thrown.
                command = PathProvider.GetDotNetPath();
                arguments = "tool install --global Microsoft.OpenApi.Kiota --version 1.22.0";
                using var context = new DependencyContext(command, $"{command} {arguments}");
                processLauncher.Start(command, arguments);
                context.Succeeded();
            }
            
        }
    }
}
