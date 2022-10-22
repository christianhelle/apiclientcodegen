using System.Diagnostics.CodeAnalysis;
using Rapicgen.CustomTool.OpenApi;

namespace Rapicgen.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class OpenApiCodeGeneratorCustomToolSetter
        : CustomToolSetter<OpenApiCodeGenerator>
    {
        public const string Name = nameof(OpenApiCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0500;
    }
}