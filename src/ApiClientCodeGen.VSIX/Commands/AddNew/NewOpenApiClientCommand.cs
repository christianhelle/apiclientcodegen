using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew
{
    public class NewOpenApiClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0500;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.OpenApi;
    }
}