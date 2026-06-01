using System.ComponentModel;
using Rapicgen.Core.External;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;

namespace ApiClientCodeGen.VSIX.Extensibility;

public class CustomDependencyInstaller(
    INpmInstaller npm,
    IFileDownloader downloader,
    IProcessLauncher processLauncher) 
    : DependencyInstaller(npm, downloader, processLauncher)
{
    private readonly IProcessLauncher processLauncher = processLauncher;

    public new void InstallKiota()
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
            if (!kiotaVersion.StartsWith(Rapicgen.Core.ExternalTools.Kiota.Version))
            { 
                //older or newer? i guess this should be handled.
            }
        }
        catch (Win32Exception)
        {
            // if command doesn't exist Win32Exception is thrown.
            command = PathProvider.GetDotNetPath();
            arguments = $"tool install --global {Rapicgen.Core.ExternalTools.Kiota.PackageId} --version {Rapicgen.Core.ExternalTools.Kiota.Version} --framework net8.0";
            using var context = new DependencyContext(command, $"{command} {arguments}");
            processLauncher.Start(command, arguments);
            context.Succeeded();
        }
    }
}
