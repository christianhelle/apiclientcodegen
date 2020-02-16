#tool "nuget:?package=Microsoft.TestPlatform&version=16.5.0"

var target = Argument("target", "Default");
var configuration = "Release";
FilePath solutionPath = File("./ApiClientCodeGenerator.sln");

Task("Clean")
    .Does(() =>
{
    // Clean directories.
    CleanDirectory("./artifacts");
    CleanDirectories("./**/bin/**");
    CleanDirectories("./**/obj/**");
    CleanDirectories("./packages/");
});

Task("Restore")
	.Does(() =>
{
	Information("Restoring solution...");
	NuGetRestore(solutionPath);
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
    VSTest("./**/bin/" + configuration + "/*Tests.dll",
           new VSTestSettings 
           { 
               Parallel = true, 
               EnableCodeCoverage = true,
               SettingsFile = File("./Tests.runsettings")
           });
});

Task("Post-Build")
    .IsDependentOn("Build")
    .Does(() => {
        CopyFileToDirectory("./ApiClientCodeGen.VSIX/bin/Release/ApiClientCodeGenerator.vsix", "./Artifacts/");
    });


Task("Default")
	// .IsDependentOn("Post-Build")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);