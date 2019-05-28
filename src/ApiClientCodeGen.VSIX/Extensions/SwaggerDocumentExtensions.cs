using System.Linq;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions
{
    public static class SwaggerDocumentExtensions
    {
        public static string GenerateClassName(this SwaggerDocument document)
            => string.IsNullOrWhiteSpace(document.Info?.Title)
                ? "ApiClient"
                : $"{GetSanitizeTitle(document)}Client";

        public static string GetSanitizeTitle(this SwaggerDocument document)
            => RemoveCharacters(
                document.Info.Title,
                "Swagger", " ", ".", "-");

        private static string RemoveCharacters(string source, params string[] removeChars)
            => removeChars.Aggregate(
                source,
                (current, c) => current.Replace(c, string.Empty));

    }
}