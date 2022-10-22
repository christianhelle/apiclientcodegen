using NJsonSchema.CodeGeneration.CSharp;

namespace Rapicgen.Core.Options.NSwag
{
    public class DefaultNSwagOptions : INSwagOptions
    {
        public bool InjectHttpClient { get; } = true;
        public bool GenerateClientInterfaces { get; } = true;
        public bool GenerateDtoTypes { get; } = true;
        public CSharpClassStyle ClassStyle { get; } = CSharpClassStyle.Poco;
        public bool UseDocumentTitle { get; } = true;
        public bool UseBaseUrl { get; }
    }
}