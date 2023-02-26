using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;

namespace Rapicgen.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewRefitterClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0800;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.Refitter;
    }
}