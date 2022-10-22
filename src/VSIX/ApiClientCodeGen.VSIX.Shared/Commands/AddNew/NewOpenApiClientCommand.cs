using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;

namespace Rapicgen.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewOpenApiClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0500;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.OpenApi;
    }
}