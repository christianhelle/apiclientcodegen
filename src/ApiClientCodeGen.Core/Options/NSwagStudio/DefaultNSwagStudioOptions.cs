using System.Diagnostics.CodeAnalysis;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio
{
    [ExcludeFromCodeCoverage]
    public class DefaultNSwagStudioOptions : INSwagStudioOptions
    {
        public bool InjectHttpClient { get; }
        public bool GenerateClientInterfaces { get; }
        public bool GenerateDtoTypes { get; }
        public bool UseBaseUrl { get; }
        public CSharpClassStyle ClassStyle { get; }
        public bool UseDocumentTitle { get; }
        public bool GenerateResponseClasses { get; }
        public bool GenerateJsonMethods { get; }
        public bool RequiredPropertiesMustBeDefined { get; }
        public bool GenerateDefaultValues { get; }
        public bool GenerateDataAnnotations { get; }
    }
}