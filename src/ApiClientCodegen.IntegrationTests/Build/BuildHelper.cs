using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build.Projects;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build
{
    public sealed class BuildHelper
    {
        public static void BuildCSharp(
            ProjectTypes projecType,
            string generatedCode,
            SupportedCodeGenerator generator)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(path);
            var projectFile = Path.Combine(path, "Project.csproj");
            var projectContents = GetProjectContents(projecType, generator);
            Trace.WriteLine(projectContents);
            File.WriteAllText(projectFile, projectContents);
            File.WriteAllText(Path.Combine(path, "Generated.cs"), generatedCode);
            new ProcessLauncher().Start("dotnet.exe", $"build \"{projectFile}\"");
        }

        private static string GetProjectContents(
            ProjectTypes projecType,
            SupportedCodeGenerator generator)
        {
            switch (generator)
            {
                case SupportedCodeGenerator.NSwag:
                case SupportedCodeGenerator.NSwagStudio:
                    switch (projecType)
                    {
                        case ProjectTypes.DotNetCoreApp:
                            return NSwagProjectFileContents.NetCoreApp;

                        case ProjectTypes.DotNetStandardLibrary:
                            return NSwagProjectFileContents.NetStandardLibrary;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(projecType), projecType, null);
                    }

                case SupportedCodeGenerator.AutoRest:
                    switch (projecType)
                    {
                        case ProjectTypes.DotNetCoreApp:
                            return AutoRestProjectFileContents.NetCoreApp;

                        case ProjectTypes.DotNetStandardLibrary:
                            return AutoRestProjectFileContents.NetStandardLibrary;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(projecType), projecType, null);
                    }

                case SupportedCodeGenerator.Swagger:
                case SupportedCodeGenerator.OpenApi:
                    switch (projecType)
                    {
                        case ProjectTypes.DotNetCoreApp:
                            return SwaggerProjectFileContents.NetCoreApp;

                        case ProjectTypes.DotNetStandardLibrary:
                            return SwaggerProjectFileContents.NetStandardLibrary;

                        case ProjectTypes.DotNetFramework:
                            return SwaggerProjectFileContents.NetFrameworkApp;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(projecType), projecType, null);
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(generator), generator, null);
            }
        }
    }
}
