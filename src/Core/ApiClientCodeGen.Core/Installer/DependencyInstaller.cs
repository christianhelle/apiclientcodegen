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
                if (!nswagVersion.Contains("14.6.3"))
                {
                    // Version mismatch, update to required version
                    UpdateNSwagTool();
                }
            }
            catch (Win32Exception)
            {
                // If command doesn't exist Win32Exception is thrown - install the tool
                InstallNSwagTool();
            }
        }

        private void InstallNSwagTool()
        {
            var command = PathProvider.GetDotNetPath();
            var arguments = "tool install --global NSwag.ConsoleCore --version 14.6.3";
            using var context = new DependencyContext(command, $"{command} {arguments}");
            processLauncher.Start(command, arguments);
            context.Succeeded();
        }

        private void UpdateNSwagTool()
        {
            var command = PathProvider.GetDotNetPath();
            var arguments = "tool update --global NSwag.ConsoleCore --version 14.6.3";
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
                if (!kiotaVersion.StartsWith("1.30.0"))
                { 
                    //older or newer? i guess this should be handled.
                }
            }
            catch (Win32Exception)
            {
                // if command doesn't exist Win32Exception is thrown.
                command = PathProvider.GetDotNetPath();
                arguments = "tool install --global Microsoft.OpenApi.Kiota --version 1.30.0";
                using var context = new DependencyContext(command, $"{command} {arguments}");
                processLauncher.Start(command, arguments);
                context.Succeeded();
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
                var requiredVersion = new Version(1, 6, 3);
                
                if (!string.IsNullOrEmpty(toolListOutput))
                {
                    var lines = toolListOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        // Look for a line containing "refitter" (case-insensitive)
                        if (line.IndexOf("refitter", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            refitterInstalled = true;
                            // Expected format: "refitter    1.7.3    refitter"
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
                    // Refitter is not installed or version is too old, install/update required version
                    var installCommand = PathProvider.GetDotNetPath();
                    var installArguments = "tool install --global refitter --version 1.7.3";
                    using var context = new DependencyContext(installCommand, $"{installCommand} {installArguments}");
                    processLauncher.Start(installCommand, installArguments);
                    context.Succeeded();
                }
            }
            catch (Win32Exception)
            {
                // If dotnet command doesn't exist or fails, install Refitter
                command = PathProvider.GetDotNetPath();
                arguments = "tool install --global refitter --version 1.7.3";
                using var context = new DependencyContext(command, $"{command} {arguments}");
                processLauncher.Start(command, arguments);
                context.Succeeded();
            }
        }
    }
}
