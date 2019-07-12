using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class NSwagCodeGeneratorCustomToolSetter
        : CustomToolSetter<NSwagCodeGenerator>
    {
        public const string Name = nameof(NSwagCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0300;
    }
}