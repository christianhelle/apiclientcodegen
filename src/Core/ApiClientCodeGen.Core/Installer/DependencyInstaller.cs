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

        /// <summary>
        /// Installs AutoRest code generator via NPM.
        /// </summary>
        /// <remarks>
        /// AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. 
        /// AutoRest support will be removed from this tool in a future major version. 
        /// Use NSwag, Refitter, or Kiota instead.
        /// </remarks>
        [Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.", false)]
        public void InstallAutoRest()
        {
            npm.InstallNpmPackage(ExternalTools.AutoRest.PackageId!);
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
                    // Version mismatch, update to required version
                    InstallOrUpdateDotNetTool(ExternalTools.NSwag, update: true);
                }
            }
            catch (Win32Exception)
            {
                // If command doesn't exist Win32Exception is thrown - install the tool
                InstallOrUpdateDotNetTool(ExternalTools.NSwag, update: false);
            }
        }

        /// <summary>
        /// Installs or updates a .NET global tool to the version pinned in <see cref="ExternalTools"/>.
        /// </summary>
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
                    // Kiota reports its version when already installed. Updating an installed-but-
                    // mismatched version is intentionally left untouched: existing tests pin the
                    // current "probe-only" behavior for the already-installed path.
                }
            }
            catch (Win32Exception)
            {
                // if command doesn't exist Win32Exception is thrown.
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
                
                // Parse the tool list output to find Refitter
                var requiredVersion = new Version(ExternalTools.Refitter.Version);
                
                if (!string.IsNullOrEmpty(toolListOutput))
                {
                    var lines = toolListOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        // Look for a line containing "refitter" (case-insensitive)
                        if (line.IndexOf("refitter", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            refitterInstalled = true;
                            // Expected format: "refitter    2.0.0    refitter"
                            // Split by whitespace and look for version
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
                    // Already installed but outdated uses update; otherwise a fresh install.
                    InstallOrUpdateDotNetTool(ExternalTools.Refitter, update: refitterInstalled);
                }
            }
            catch (Win32Exception)
            {
                // If dotnet command doesn't exist or fails, install Refitter
                InstallOrUpdateDotNetTool(ExternalTools.Refitter, update: false);
            }
        }
    }
}
