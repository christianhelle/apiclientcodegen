using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewNSwagClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0300;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.NSwag;
    }
}