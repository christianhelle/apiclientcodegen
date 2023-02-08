using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Rapicgen.Core.Logging;
using NSwag;

namespace Rapicgen.Core.Extensions
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
                        .Replace(".json", string.Empty)
                        .Replace(".yaml", string.Empty)
                        .Replace(".yml", string.Empty);
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Logger.Instance.WriteLine("Unable to extract class name from document path: " + document.DocumentPath);
            }

            return string.IsNullOrWhiteSpace(document.Info?.Title)
                ? "ApiClient"
                : $"{GetSanitizeTitle(document)}Client";
        }

        private static string GetSanitizeTitle(this OpenApiDocument document)
            => RemoveCharacters(
                document.Info.Title,
                "Swagger", " ", ".", "-");

        private static string RemoveCharacters(string source, params string[] removeChars)
            => removeChars.Aggregate(
                source,
                (current, c) => current.Replace(c, string.Empty));

    }
}