var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Task("Clean")
    .Does(() =>
{
    CleanDirectory("./artifacts");
    CleanDirectories("./**/bin/**");
    CleanDirectories("./**/obj/**");
    CleanDirectories("./packages/");
    CleanDirectories("./generated/");
    CleanDirectories("./TestResults/");
});

Task("Restore")
    .Does(() =>
{
    var solutions = GetFiles("./**/*.slnx");
    foreach(var solution in solutions)
    {
        if (solution.ToString().Contains("Mac.slnx"))
            continue;
        Information("Restoring {0}", solution);
        DotNetRestore(solution.ToString());
    }
});

Task("Build-VSIX")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building VSIX solution with native MSBuild");
        
        // Try to use Cake's VSWhereLatest to find Visual Studio
        FilePath msbuildPath = null;
        DirectoryPath vsInstallPath = null;
        
        try
        {
            Information("Attempting to locate Visual Studio using VSWhereLatest");
            vsInstallPath = VSWhereLatest(new VSWhereLatestSettings
            {
                Requires = "Microsoft.Component.MSBuild"
            });
            
            if (vsInstallPath != null)
            {
                Information($"Found Visual Studio at: {vsInstallPath}");
                
                // Try amd64 version first
                var candidatePath = vsInstallPath.CombineWithFilePath("MSBuild/Current/Bin/amd64/MSBuild.exe");
                if (FileExists(candidatePath))
                {
                    msbuildPath = candidatePath;
                }
                else
                {
                    // Try x86 version
                    candidatePath = vsInstallPath.CombineWithFilePath("MSBuild/Current/Bin/MSBuild.exe");
                    if (FileExists(candidatePath))
                    {
                        msbuildPath = candidatePath;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Warning($"VSWhereLatest failed: {ex.Message}");
        }
        
        // Fallback to searching common paths if VSWhereLatest fails
        if (msbuildPath == null)
        {
            Warning("VSWhereLatest not available or failed, falling back to common paths");
            var programFiles = Environment.GetEnvironmentVariable("ProgramFiles");
            var programFilesX86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            
            var commonPaths = new List<string>();
            
            // Add paths for both Program Files locations
            // Prioritize full editions over BuildTools
            var editions = new[] { "Enterprise", "Professional", "Community", "Preview", "Insiders", "BuildTools" };
            var versions = new[] { "2022", "2019", "18" };  // 18 is for Insiders preview
            
            foreach (var baseDir in new[] { programFiles, programFilesX86 })
            {
                if (!string.IsNullOrEmpty(baseDir))
                {
                    foreach (var version in versions)
                    {
                        foreach (var edition in editions)
                        {
                            commonPaths.Add(System.IO.Path.Combine(baseDir, "Microsoft Visual Studio", version, edition, "MSBuild", "Current", "Bin", "amd64", "MSBuild.exe"));
                            commonPaths.Add(System.IO.Path.Combine(baseDir, "Microsoft Visual Studio", version, edition, "MSBuild", "Current", "Bin", "MSBuild.exe"));
                        }
                    }
                }
            }
            
            foreach(var path in commonPaths)
            {
                if (FileExists(path))
                {
                    msbuildPath = path;
                    Information($"Found MSBuild at common path: {path}");
                    break;
                }
            }
        }
        
        if (msbuildPath == null)
        {
            throw new Exception("Could not find MSBuild.exe. Please ensure Visual Studio 2019 or later is installed with MSBuild component.");
        }
        
        Information($"Using MSBuild from: {msbuildPath}");
        var exitCode = StartProcess(msbuildPath, new ProcessSettings
        {
            Arguments = new ProcessArgumentBuilder()
                .Append("VSIX.slnx")
                .Append("/t:Rebuild")
                .Append($"/p:Configuration={configuration}")
                .Append("/p:DeployExtension=false")
                .Append("/m")
        });
        if (exitCode != 0)
        {
            throw new Exception($"MSBuild failed with exit code {exitCode}");
        }

        // Copy VSIX files to artifacts folder
        Information("Copying VSIX files to artifacts folder");
        EnsureDirectoryExists("./artifacts");
        var vsixFiles = GetFiles($"./VSIX/**/bin/{configuration}/*.vsix");
        foreach(var vsixFile in vsixFiles)
        {
            var projectName = vsixFile.GetDirectory().GetParent().GetParent().GetDirectoryName();
            var artifactName = $"{projectName}-{configuration}.vsix";
            var destinationPath = $"./artifacts/{artifactName}";
            Information($"Copying {vsixFile.GetFilename()} to {destinationPath}");
            CopyFile(vsixFile, destinationPath);
        }
        
        if (vsixFiles.Count() > 0)
        {
            Information($"Successfully copied {vsixFiles.Count()} VSIX file(s) to artifacts folder");
        }
        else
        {
            Warning("No VSIX files found to copy");
        }
    });

Task("Build-Rapicgen")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building Rapicgen (.NET Tool)");
        DotNetBuild("Rapicgen.slnx", new DotNetBuildSettings
        {
            Configuration = configuration,
            NoRestore = true,
            MSBuildSettings = new DotNetMSBuildSettings()
                .SetMaxCpuCount(0)
        });
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build-Rapicgen")
    .Does(() =>
{
    var testProjects = GetFiles("./**/ApiClientCodeGen.*.Tests.csproj");
    foreach(var project in testProjects)
    {
        Information("Running tests for {0}", project);
        DotNetTest(project.ToString(), new DotNetTestSettings
        {
            Configuration = "Release",
            NoBuild = true,
            NoRestore = true,
            Loggers = new[] { "console;verbosity=minimal" }
        });
    }
});

Task("Run-Integration-Tests")
    .IsDependentOn("Build-Rapicgen")
    .Does(() =>
{
    var testProjects = GetFiles("./**/ApiClientCodeGen.*.IntegrationTests.csproj");
    foreach(var project in testProjects)
    {
        Information("Running integration tests for {0}", project);
        DotNetTest(project.ToString(), new DotNetTestSettings
        {
            Configuration = "Release",
            NoBuild = true,
            NoRestore = true,
            Loggers = new[] { "console;verbosity=minimal" }
        });
    }
});

Task("Run-VSIX-Unit-Tests")
    .IsDependentOn("Build-Rapicgen")
    .Does(() =>
{
    var testProjects = GetFiles("./VSIX/**/ApiClientCodeGen.*.Tests.csproj");
    foreach(var project in testProjects)
    {
        Information("Running VSIX tests for {0}", project);
        DotNetTest(project.ToString(), new DotNetTestSettings
        {
            Configuration = "Release",
            NoBuild = true,
            NoRestore = true,
            Loggers = new[] { "console;verbosity=minimal" }
        });
    }
});

Task("Run-VSIX-Integration-Tests")
    .IsDependentOn("Build-Rapicgen")
    .Does(() =>
{
    var testProjects = GetFiles("./VSIX/**/ApiClientCodeGen.*.IntegrationTests.csproj");
    foreach(var project in testProjects)
    {
        Information("Running VSIX integration tests for {0}", project);
        DotNetTest(project.ToString(), new DotNetTestSettings
        {
            Configuration = "Release",
            NoBuild = true,
            NoRestore = true,
            Loggers = new[] { "console;verbosity=minimal" }
        });
    }
});

Task("Run-All-Tests")
    .IsDependentOn("Build-Rapicgen")
    .Does(() =>
{
    var testProjects = GetFiles("./**/*Tests.csproj");
    foreach(var project in testProjects)
    {
        Information("Running tests for {0}", project);
        DotNetTest(project.ToString(), new DotNetTestSettings
        {
            Configuration = "Release",
            NoBuild = true,
            NoRestore = true,
            Loggers = new[] { "console;verbosity=minimal" }
        });
    }
});

Task("All")
    .IsDependentOn("Build-VSIX")
    .IsDependentOn("Run-All-Tests");

Task("VSIX")
    .IsDependentOn("Build-VSIX")
    .IsDependentOn("Run-VSIX-Unit-Tests")
    .IsDependentOn("Run-VSIX-Integration-Tests");

Task("Rapicgen")
    .IsDependentOn("Build-Rapicgen");

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);