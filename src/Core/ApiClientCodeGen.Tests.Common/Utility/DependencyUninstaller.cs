using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;

namespace ApiClientCodeGen.Tests.Common.Utility
{
    [ExcludeFromCodeCoverage]
    public static class DependencyUninstaller
    {
        public static void UninstallOpenApiGenerator() 
            => File.Delete(Path.Combine(Path.GetTempPath(), "openapi-generator-cli.jar"));

        public static void UninstallSwaggerCodegen() 
            => File.Delete(Path.Combine(Path.GetTempPath(), "swagger-codegen-cli.jar"));
    }
}
