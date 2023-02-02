using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;

namespace Rapicgen.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewKiotaClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0700;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.Kiota;
    }
}