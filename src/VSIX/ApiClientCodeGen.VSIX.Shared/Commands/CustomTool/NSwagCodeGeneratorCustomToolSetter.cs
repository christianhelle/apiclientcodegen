using System.Diagnostics.CodeAnalysis;
using Rapicgen.CustomTool.NSwag;

namespace Rapicgen.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class NSwagCodeGeneratorCustomToolSetter
        : CustomToolSetter<NSwagCodeGenerator>
    {
        public const string Name = nameof(NSwagCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0300;
    }
}