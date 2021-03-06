using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    [Obsolete]
    public static class DependencyDownloader
    {
        private static readonly IDependencyInstaller installer
            = new DependencyInstaller(
                new NpmInstaller(new ProcessLauncher()),
                new FileDownloader(new WebDownloader()));

        public static void InstallAutoRest() => installer.InstallAutoRest().GetAwaiter().GetResult();

        public static void InstallNSwag() => installer.InstallNSwag().GetAwaiter().GetResult();

        public static string InstallOpenApiGenerator(string path = null, bool forceDownload = false)
            => installer.InstallOpenApiGenerator().GetAwaiter().GetResult();

        public static string InstallSwaggerCodegenCli(string path = null, bool forceDownload = false)
            => installer.InstallSwaggerCodegen().GetAwaiter().GetResult();
    }
}