using System;
using System.Linq;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag
{
    public interface INSwagCodeGeneratorSettingsFactory
    {
        CSharpClientGeneratorSettings GetGeneratorSettings(OpenApiDocument document);
    }

    public class NSwagCodeGeneratorSettingsFactory : INSwagCodeGeneratorSettingsFactory
    {
        private readonly string defaultNamespace;
        private readonly INSwagOptions options;

        public NSwagCodeGeneratorSettingsFactory(string defaultNamespace, INSwagOptions options)
        {
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public CSharpClientGeneratorSettings GetGeneratorSettings(OpenApiDocument document)
            => new CSharpClientGeneratorSettings
            {
                ClassName = GetClassName(document),
                InjectHttpClient = options.InjectHttpClient,
                GenerateClientInterfaces = options.GenerateClientInterfaces,
                GenerateDtoTypes = options.GenerateDtoTypes,
                UseBaseUrl = options.UseBaseUrl,
                CSharpGeneratorSettings =
                {
                    Namespace = defaultNamespace,
                    ClassStyle = options.ClassStyle
                },
            };

        private static string GetClassName(OpenApiDocument document)
            => string.IsNullOrWhiteSpace(document.Info?.Title)
                ? "ApiClient"
                : $"{SanitizeTitle(document)}Client";

        private static string SanitizeTitle(OpenApiDocument document)
            => RemoveCharacters(
                document.Info.Title,
                "Swagger", " ", ".", "-");

        private static string RemoveCharacters(string source, params string[] removeChars)
            => removeChars.Aggregate(
                source,
                (current, c) => current.Replace(c, string.Empty));
    }
}