using System;
using System.ComponentModel;
using Rapicgen.Core.External;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;

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

        public string InstallOpenApiGenerator(OpenApiSupportedVersion version = default)
        {
            var openApiGeneratorVersion = OpenApiGeneratorVersions.GetVersion(version);
            return downloader.DownloadFile(
                $"openapi-generator-cli-{openApiGeneratorVersion.Version}.jar",
                openApiGeneratorVersion.SHA1,
                openApiGeneratorVersion.DownloadUrl);
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
                if (!kiotaVersion.StartsWith("1.28.0"))
                { 
                    //older or newer? i guess this should be handled.
                }
            }
            catch (Win32Exception e)
            {
                //if command doesn't exist Win32Exception is thrown.
                command = PathProvider.GetDotNetPath();
                arguments = "tool install --global Microsoft.OpenApi.Kiota --version 1.28.0";
                using var context = new DependencyContext(command, $"{command} {arguments}");
                processLauncher.Start(command, arguments);
                context.Succeeded();
            }
        }

        public void InstallRefitter()
        {
            var command = "refitter";
            string arguments = "--version";
            string refitterVersion = "";
            try
            {
                processLauncher.Start(command, arguments, output =>
                {
                    if (output != null)
                    {
                        refitterVersion = output ?? refitterVersion;
                        Logger.Instance.WriteLine(output);
                    }
                }, error =>
                {
                    if (error != null)
                    {
                        Logger.Instance.WriteLine(error);
                    }
                });
                if (!refitterVersion.StartsWith("1.6."))
                { 
                    //older or newer? i guess this should be handled.
                }
            }
            catch (Win32Exception e)
            {
                //if command doesn't exist Win32Exception is thrown.
                command = PathProvider.GetDotNetPath();
                arguments = "tool install --global refitter --version 1.6.2";
                using var context = new DependencyContext(command, $"{command} {arguments}");
                processLauncher.Start(command, arguments);
                context.Succeeded();
            }
        }
    }
}
