using Rapicgen.Core.Options.NSwag;

namespace Rapicgen.Core.Options.NSwagStudio
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