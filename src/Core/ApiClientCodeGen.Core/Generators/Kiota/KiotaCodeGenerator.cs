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
            
            processLauncher.Start(
                PathProvider.GetDotNetPath(),
                $"kiota generate -l CSharp -d {swaggerFile} -o {outputPath}");
            
            pGenerateProgress?.Progress(80);
            
            return CSharpFileMerger.MergeFilesAndDeleteSource(outputPath);
        }
        finally
        {
            pGenerateProgress?.Progress(100);
        }
    }
}