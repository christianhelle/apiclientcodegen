namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public interface INSwagStudioOptions : INSwagOptions
    {
        bool GenerateResponseClasses { get; set; }
        bool GenerateJsonMethods { get; set; }
        bool RequiredPropertiesMustBeDefined { get; set; }
        bool GenerateDefaultValues { get; set; }
        bool GenerateDataAnnotations { get; set; }
    }
}