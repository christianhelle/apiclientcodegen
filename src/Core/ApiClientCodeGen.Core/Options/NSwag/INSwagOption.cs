using NJsonSchema.CodeGeneration.CSharp;

namespace Rapicgen.Core.Options.NSwag
{
    public interface INSwagOptions
    {
        bool InjectHttpClient { get; }
        bool GenerateClientInterfaces { get; }
        bool GenerateDtoTypes { get; }
        bool UseBaseUrl { get; }
        CSharpClassStyle ClassStyle { get; }
        bool UseDocumentTitle { get; }
    }
}