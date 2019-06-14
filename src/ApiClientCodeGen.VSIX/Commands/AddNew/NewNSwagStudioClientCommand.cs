using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewNSwagStudioClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0600;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.NSwagStudio;
    }
}