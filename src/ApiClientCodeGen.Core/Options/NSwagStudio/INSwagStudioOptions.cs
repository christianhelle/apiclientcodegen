using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwagStudio
{
    public interface INSwagStudioOptions : INSwagOptions
    {
        bool GenerateResponseClasses { get; }
        bool GenerateJsonMethods { get; }
        bool RequiredPropertiesMustBeDefined { get; }
        bool GenerateDefaultValues { get; }
        bool GenerateDataAnnotations { get; }
    }
}