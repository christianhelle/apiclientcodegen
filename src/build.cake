#tool "nuget:?package=Microsoft.TestPlatform&version=16.5.0"

var target = Argument("target", "Default");
var configuration = "Release";
FilePath solutionPath = File("./All.sln");

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
        Information("Restoring {0}", solution);
        DotNetCoreRestore(solution.ToString());
    }
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building solution...");
        MSBuild(solutionPath, settings =>
            settings.SetPlatformTarget(PlatformTarget.MSIL)
                .SetMSBuildPlatform(MSBuildPlatform.x86)
                .UseToolVersion(MSBuildToolVersion.VS2019)
                .WithTarget("Build")
                .SetConfiguration(configuration));
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    VSTest("./**/bin/" + configuration + "/*.Tests.dll",
           new VSTestSettings 
           { 
               Parallel = true, 
               EnableCodeCoverage = true,
               SettingsFile = File("./Tests.runsettings")
           });
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);