using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.OpenApi;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.CustomTool
{
    public class OpenApiCodeGeneratorCustomToolSetter
        : CustomToolSetter<OpenApiCodeGenerator>
    {
        public const string Name = nameof(OpenApiCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0500;
    }
}