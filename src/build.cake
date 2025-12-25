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
    var solutions = GetFiles("./**/*.sln");
    foreach(var solution in solutions)
    {
        if (solution.ToString().Contains("Mac.sln"))
            continue;
        Information("Restoring {0}", solution);
        DotNetRestore(solution.ToString());
    }
});

Task("Build-VSIX")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building VSIX solution with native MSBuild");
        var msbuildPath = @"C:\Program Files\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin\amd64\MSBuild.exe";
        if (!FileExists(msbuildPath))
        {
            // Fallback to VS2022 path
            msbuildPath = @"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\amd64\MSBuild.exe";
            if (!FileExists(msbuildPath))
            {
                msbuildPath = @"C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\amd64\MSBuild.exe";
                if (!FileExists(msbuildPath))
                {
                    msbuildPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe";
                }
            }
        }
        
        Information($"Using MSBuild from: {msbuildPath}");
        var exitCode = StartProcess(msbuildPath, new ProcessSettings
        {
            Arguments = new ProcessArgumentBuilder()
                .Append("VSIX.sln")
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
        DotNetBuild("Rapicgen.sln", new DotNetBuildSettings
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