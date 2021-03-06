using System;
using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer
{
    public class DependencyInstaller : IDependencyInstaller
    {
        private readonly INpmInstaller npm;
        private readonly IFileDownloader downloader;

        public DependencyInstaller(INpmInstaller npm, IFileDownloader downloader)
        {
            this.npm = npm ?? throw new ArgumentNullException(nameof(npm));
            this.downloader = downloader ?? throw new ArgumentNullException(nameof(downloader));
        }

        public Task InstallAutoRest()
        {
            return npm.InstallNpmPackage("autorest");
        }

        public Task InstallNSwag()
        {
            return npm.InstallNpmPackage("nswag");
        }

        public Task<string> InstallOpenApiGenerator()
        {
            return downloader.DownloadFile(
                null,
                "openapi-generator-cli.jar",
                Resource.OpenApiGenerator_MD5,
                Resource.OpenApiGenerator_DownloadUrl);
        }

        public Task<string> InstallSwaggerCodegen()
        {
            return downloader.DownloadFile(
                null,
                "swagger-codegen-cli.jar",
                Resource.SwaggerCodegenCli_MD5,
                Resource.SwaggerCodegenCli_DownloadUrl);
        }
    }
}