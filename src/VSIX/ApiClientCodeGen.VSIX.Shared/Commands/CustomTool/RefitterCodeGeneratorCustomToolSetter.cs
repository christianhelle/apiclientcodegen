using System.Diagnostics.CodeAnalysis;
using Rapicgen.CustomTool.Refitter;

namespace Rapicgen.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class RefitterCodeGeneratorCustomToolSetter
        : CustomToolSetter<RefitterCodeGenerator>
    {
        public const string Name = nameof(RefitterCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0700;
    }
}