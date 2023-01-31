using System.Diagnostics.CodeAnalysis;
using Rapicgen.CustomTool.Kiota;

namespace Rapicgen.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class KiotaCodeGeneratorCustomToolSetter
        : CustomToolSetter<KiotaCodeGenerator>
    {
        public const string Name = nameof(KiotaCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0600;
    }
}