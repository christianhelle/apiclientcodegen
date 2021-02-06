using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class AutoRestCodeGeneratorCustomToolSetter
        : CustomToolSetter<AutoRestCodeGenerator>
    {
        public const string Name = nameof(AutoRestCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0200;
    }
}