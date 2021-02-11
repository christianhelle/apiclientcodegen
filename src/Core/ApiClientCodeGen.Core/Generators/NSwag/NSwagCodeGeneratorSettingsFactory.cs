using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag
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
                ClassName = document.GenerateClassName(options.UseDocumentTitle),
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
    }
}