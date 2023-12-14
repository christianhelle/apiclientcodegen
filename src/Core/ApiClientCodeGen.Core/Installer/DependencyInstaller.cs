using System;
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
            try
            {
                var command = PathProvider.GetDotNetPath();
                const string arguments = "tool install --global Microsoft.OpenApi.Kiota --version 1.9.1";
                using var context = new DependencyContext(command, $"{command} {arguments}");
                processLauncher.Start(command, arguments);
                context.Succeeded();
            }
            catch (ProcessLaunchException e)
            {
                if (e.ErrorData?.Contains("'microsoft.openapi.kiota'") != true)
                    throw;
            }
        }
    }
}