using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;

namespace Rapicgen.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewSwaggerClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0400;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.Swagger;
    }
}