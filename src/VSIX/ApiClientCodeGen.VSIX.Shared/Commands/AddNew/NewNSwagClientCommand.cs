using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;

namespace Rapicgen.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewNSwagClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0300;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.NSwag;
    }
}