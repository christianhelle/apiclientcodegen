using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew
{
    public class NewNSwagStudioClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0600;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.NSwagStudio;
    }
}