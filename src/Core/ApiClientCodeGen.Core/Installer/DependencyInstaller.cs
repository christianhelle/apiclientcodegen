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

        public void InstallNSwag()
        {
            var command = "nswag";
            string arguments = "version";
            string nswagVersion = "";
            try
            {
                processLauncher.Start(command, arguments, output =>
                {
                    if (output == null) return;
                    nswagVersion = output;
                    Logger.Instance.WriteLine(nswagVersion);
                }, error =>
                {
                    if (error != null)
                    {
                        Logger.Instance.WriteLine(error);
                    }
                });
                if (!nswagVersion.Contains(ExternalTools.NSwag.Version))
                {
                    InstallOrUpdateDotNetTool(ExternalTools.NSwag, update: true);
                }
            }
            catch (Win32Exception)
            {
                InstallOrUpdateDotNetTool(ExternalTools.NSwag, update: false);
            }
        }

        private void InstallOrUpdateDotNetTool(ExternalTool tool, bool update)
        {
            var command = PathProvider.GetDotNetPath();
            var verb = update ? "update" : "install";
            var arguments = $"tool {verb} --global {tool.PackageId} --version {tool.Version}";
            using var context = new DependencyContext(command, $"{command} {arguments}");
            processLauncher.Start(command, arguments);
            context.Succeeded();
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
                    if (output == null) return;
                    kiotaVersion = output;
                    Logger.Instance.WriteLine(kiotaVersion);
                }, error =>
                {
                    if (error != null)
                    {
                        Logger.Instance.WriteLine(error);
                    }
                });
                if (!kiotaVersion.StartsWith(ExternalTools.Kiota.Version))
                {
                }
            }
            catch (Win32Exception)
            {
                InstallOrUpdateDotNetTool(ExternalTools.Kiota, update: false);
            }
        }

        public void InstallRefitter()
        {
            var command = PathProvider.GetDotNetPath();
            string arguments = "tool list --global";
            string toolListOutput = "";
            Version? installedVersion = null;
            bool refitterInstalled = false;

            try
            {
                processLauncher.Start(command, arguments, output =>
                {
                    if (output != null)
                    {
                        toolListOutput += output + Environment.NewLine;
                        Logger.Instance.WriteLine(output);
                    }
                }, error =>
                {
                    if (error != null)
                    {
                        Logger.Instance.WriteLine(error);
                    }
                });

                var requiredVersion = new Version(ExternalTools.Refitter.Version);

                if (!string.IsNullOrEmpty(toolListOutput))
                {
                    var lines = toolListOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        if (line.IndexOf("refitter", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            refitterInstalled = true;
                            var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var part in parts)
                            {
                                if (Version.TryParse(part, out var v))
                                {
                                    installedVersion = v;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                if (!refitterInstalled || installedVersion == null || installedVersion < requiredVersion)
                {
                    InstallOrUpdateDotNetTool(ExternalTools.Refitter, update: refitterInstalled);
                }
            }
            catch (Win32Exception)
            {
                InstallOrUpdateDotNetTool(ExternalTools.Refitter, update: false);
            }
        }
    }
}
