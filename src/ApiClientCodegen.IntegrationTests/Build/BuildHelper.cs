using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build
{
    public sealed class BuildHelper
    {
        public static void BuildCSharp(ProjectTypes projecType, string generatedCode)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(path);
            var projectFile = Path.Combine(path, "Project.csproj");
            var projectContents = GetProjectContents(projecType);
            Trace.WriteLine(projectContents);
            File.WriteAllText(projectFile, projectContents);
            File.WriteAllText(Path.Combine(path, "Generated.cs"), generatedCode);
            ProcessHelper.StartProcess("dotnet.exe", $"build \"{projectFile}\"");
        }

        private static string GetProjectContents(ProjectTypes projecType)
        {
            switch (projecType)
            {
                case ProjectTypes.DotNetCoreApp:
                    return ProjectFileContents.NetCoreApp;
                
                case ProjectTypes.DotNetStandardLibrary:
                    return ProjectFileContents.NetStandardLibrary;

                default:
                    throw new ArgumentOutOfRangeException(nameof(projecType), projecType, null);
            }
        }
    }
}
