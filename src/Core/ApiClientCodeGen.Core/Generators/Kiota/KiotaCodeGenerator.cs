using System;
using System.IO;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.Generators.Kiota;

public class KiotaCodeGenerator : ICodeGenerator
{
    private readonly string swaggerFile;
    private readonly string outputPath;
    private readonly IProcessLauncher processLauncher;
    private readonly IDependencyInstaller dependencyInstaller;

    public KiotaCodeGenerator(
        string swaggerFile,
        string outputPath,
        IProcessLauncher processLauncher,
        IDependencyInstaller dependencyInstaller)
    {
        this.swaggerFile = swaggerFile;
        this.outputPath = outputPath;
        this.processLauncher = processLauncher;
        this.dependencyInstaller = dependencyInstaller;
    }
    
    public string GenerateCode(IProgressReporter? pGenerateProgress)
    {
        try
        {
            pGenerateProgress?.Progress(10);
            dependencyInstaller.InstallKiota();
            
            pGenerateProgress?.Progress(30);
            var outputFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            processLauncher.Start("kiota", $" generate -l CSharp -d {swaggerFile} -o {outputFolder}");
            
            pGenerateProgress?.Progress(80);            
            return CSharpFileMerger.MergeFiles(outputFolder);
        }
        finally
        {
            pGenerateProgress?.Progress(100);
        }
    }
}