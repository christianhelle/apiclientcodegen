using System.Diagnostics.CodeAnalysis;
using Rapicgen.CustomTool.Swagger;

namespace Rapicgen.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class SwaggerCodeGeneratorCustomToolSetter
        : CustomToolSetter<SwaggerCodeGenerator>
    {
        public const string Name = nameof(SwaggerCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0400;
    }
}