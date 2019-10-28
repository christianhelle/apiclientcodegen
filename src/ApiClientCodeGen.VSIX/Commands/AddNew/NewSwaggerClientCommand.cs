using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewSwaggerClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0400;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.Swagger;
    }
}