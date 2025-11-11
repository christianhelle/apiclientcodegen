#tool "nuget:?package=Microsoft.TestPlatform&version=18.0.1"

var target = Argument("target", "Default");

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

Task("Build-Release")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building solutions");
        foreach(var solution in GetFiles("./**/*.sln"))
        {
            if (solution.ToString().Contains("Mac.sln"))
                continue;
            Information("Building {0}", solution);
            MSBuild(
                solution, 
                settings =>
                    settings.SetPlatformTarget(PlatformTarget.MSIL)
                        .SetMSBuildPlatform(MSBuildPlatform.x86)
                        .UseToolVersion(MSBuildToolVersion.VS2019)
                        .WithTarget("Build")
                        .SetConfiguration("Release"));
        }
    });

Task("Build-Debug")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building solutions");
        foreach(var solution in GetFiles("./**/*.sln"))
        {
            if (solution.ToString().Contains("Mac.sln"))
                continue;
            Information("Building {0}", solution);
            MSBuild(
                solution, 
                settings =>
                    settings.SetPlatformTarget(PlatformTarget.MSIL)
                        .SetMSBuildPlatform(MSBuildPlatform.x86)
                        .UseToolVersion(MSBuildToolVersion.VS2019)
                        .WithTarget("Build")
                        .SetConfiguration("Debug"));
        }
    });

Task("Build-VSIX")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building VSIX");
        MSBuild(
            File("VSIX.sln"),
            settings =>
                settings.SetPlatformTarget(PlatformTarget.MSIL)
                    .SetMSBuildPlatform(MSBuildPlatform.x86)
                    .UseToolVersion(MSBuildToolVersion.VS2019)
                    .WithTarget("Build")
                    .SetConfiguration("Release"));
    });

Task("Build-Rapicgen")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building Rapicgen (.NET Tool)");
        MSBuild(
            File("Rapicgen.sln"),
            settings =>
                settings.SetPlatformTarget(PlatformTarget.MSIL)
                    .SetMSBuildPlatform(MSBuildPlatform.x86)
                    .UseToolVersion(MSBuildToolVersion.VS2019)
                    .WithTarget("Build")
                    .SetConfiguration("Release"));
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build-Release")
    .Does(() =>
{
    VSTest("./**/bin/**/*.Tests.dll",
           new VSTestSettings 
           { 
               Parallel = true, 
               EnableCodeCoverage = true,
               SettingsFile = File("./Tests.runsettings")
           }.WithVisualStudioLogger());
});

Task("Run-Integration-Tests")
    .IsDependentOn("Build-Release")
    .Does(() =>
{
    VSTest("./**/bin/**/*.IntegrationTests.dll",
           new VSTestSettings 
           { 
               Parallel = true, 
               EnableCodeCoverage = true,
               SettingsFile = File("./Tests.runsettings")
           }.WithVisualStudioLogger());
});

Task("Run-VSIX-Unit-Tests")
    .IsDependentOn("Build-Release")
    .Does(() =>
{
    VSTest("./VSIX/**/bin/**/*.Tests.dll",
           new VSTestSettings 
           { 
               Parallel = true, 
               EnableCodeCoverage = true,
               SettingsFile = File("./Tests.runsettings")
           }.WithVisualStudioLogger());
});

Task("Run-VSIX-Integration-Tests")
    .IsDependentOn("Build-Release")
    .Does(() =>
{
    VSTest("./VSIX/**/bin/**/*.IntegrationTests.dll",
           new VSTestSettings 
           { 
               Parallel = true, 
               EnableCodeCoverage = true,
               SettingsFile = File("./Tests.runsettings")
           }.WithVisualStudioLogger());
});

Task("Run-All-Tests")
    .Does(() =>
{
    VSTest("./**/bin/**/*Tests.dll",
           new VSTestSettings 
           { 
               Parallel = true, 
               EnableCodeCoverage = true,
               SettingsFile = File("./Tests.runsettings")
           }.WithVisualStudioLogger());
});

Task("All")
    .IsDependentOn("Build-Debug")
    .IsDependentOn("Build-Release")
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