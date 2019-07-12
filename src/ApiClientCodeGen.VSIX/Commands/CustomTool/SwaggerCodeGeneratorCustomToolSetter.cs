using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class SwaggerCodeGeneratorCustomToolSetter
        : CustomToolSetter<SwaggerCodeGenerator>
    {
        public const string Name = nameof(SwaggerCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0400;
    }
}