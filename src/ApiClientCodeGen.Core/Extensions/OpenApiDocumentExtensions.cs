using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions
{
    public static class OpenApiDocumentExtensions
    {
        public static string GenerateClassName(this OpenApiDocument document, bool useDocumentTitle = true)
        {
            try
            {
                if (!useDocumentTitle)
                    return new FileInfo(document.DocumentPath)
                        .Name
                        .Replace(".json", string.Empty);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Unable to extract class name from document path: " + document.DocumentPath);
                Trace.WriteLine(e);
            }

            return string.IsNullOrWhiteSpace(document.Info?.Title)
                ? "ApiClient"
                : $"{GetSanitizeTitle(document)}Client";
        }

        public static string GetSanitizeTitle(this OpenApiDocument document)
            => RemoveCharacters(
                document.Info.Title,
                "Swagger", " ", ".", "-");

        private static string RemoveCharacters(string source, params string[] removeChars)
            => removeChars.Aggregate(
                source,
                (current, c) => current.Replace(c, string.Empty));

    }
}