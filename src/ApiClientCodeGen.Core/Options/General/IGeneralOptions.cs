namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General
{
    public interface IGeneralOptions
    {
        string JavaPath { get; set; }
        string NpmPath { get; set; }
        string NSwagPath { get; set; }
        string SwaggerCodegenPath { get; set; }
        string OpenApiGeneratorPath { get; set; }
    }
}