using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public interface INSwagOption
    {
        bool InjectHttpClient { get; set; }
        bool GenerateClientInterfaces { get; set; }
        bool GenerateDtoTypes { get; set; }
        bool UseBaseUrl { get; set; }
        CSharpClassStyle ClassStyle { get; set; }
    }
}