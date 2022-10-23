namespace Rapicgen.Core.Options.General
{
    public interface IGeneralOptions
    {
        string JavaPath { get; }
        string NpmPath { get; }
        string NSwagPath { get; }
        string SwaggerCodegenPath { get; }
        string OpenApiGeneratorPath { get; }
        bool? InstallMissingPackages { get; }
    }
}