using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using ApiClientCodeGen.Tests.Common.Build.Projects;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;

namespace ApiClientCodeGen.Tests.Common.Build
{
    [ExcludeFromCodeCoverage]
    public static class BuildHelper
    {
        public static bool BuildCSharp(
            ProjectTypes projecType,
            string generatedCode,
            SupportedCodeGenerator generator)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(path);
                var projectFile = Path.Combine(path, "Project.csproj");
                var projectContents = GetProjectContents(projecType, generator);
                Logger.Instance.WriteLine(projectContents);
                File.WriteAllText(projectFile, projectContents);
                File.WriteAllText(Path.Combine(path, "Generated.cs"), generatedCode);
                new ProcessLauncher().Start(GetDotNetCli(), $"build \"{projectFile}\"");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private static string GetDotNetCli()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                return "dotnet";
            else
                return "dotnet.exe";
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
                        case ProjectTypes.DotNetStandardLibrary:
                            return NSwagProjectFileContents.NetCoreApp;

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

                case SupportedCodeGenerator.OpenApi:
                    switch (projecType)
                    {
                        case ProjectTypes.DotNetCoreApp:
                            return OpenApiGeneratorProjectFileContents.NetCoreApp;

                        case ProjectTypes.DotNetStandardLibrary:
                            return OpenApiGeneratorProjectFileContents.NetStandardLibrary;

                        case ProjectTypes.DotNetFramework:
                            return OpenApiGeneratorProjectFileContents.NetFrameworkApp;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(projecType), projecType, null);
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(generator), generator, null);
            }
        }
    }
}
