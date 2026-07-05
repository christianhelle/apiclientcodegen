using Rapicgen.Core.Installer;

namespace Rapicgen.Core
{
    /// <summary>
    /// Describes an external code-generation tool wrapped by this product: its display name,
    /// the pinned version, and (for .NET/npm tools) the package id used to install it.
    /// </summary>
    /// <remarks>
    /// This is the single source of truth for an external tool's identity. Routing every version
    /// string, install command and generated-code header through <see cref="ExternalTools"/> keeps
    /// a tool upgrade a one-line change instead of a find-and-replace across Core, CLI and the IDE
    /// extensions.
    /// </remarks>
    public sealed record ExternalTool(string DisplayName, string Version, string? PackageId = null)
    {
        /// <summary>The version formatted as a header/UI label, e.g. <c>v14.7.1</c>.</summary>
        public string VersionLabel => Version.StartsWith("v") ? Version : $"v{Version}";
    }

    /// <summary>
    /// Registry of the external tools wrapped by this product and their pinned versions.
    /// </summary>
    public static class ExternalTools
    {
        public static readonly ExternalTool NSwag = new("NSwag", "14.7.1", "NSwag.ConsoleCore");

        public static readonly ExternalTool Kiota = new("Kiota", "1.32.4", "Microsoft.OpenApi.Kiota");

        public static readonly ExternalTool Refitter = new("Refitter", "2.0.0", "refitter");

        public static readonly ExternalTool SwaggerCodegen = new("Swagger Codegen CLI", "3.0.34");

        public static ExternalTool OpenApiGenerator =>
            new("OpenAPI Generator", OpenApiGeneratorVersions.GetLatestVersion().Version);
    }
}
