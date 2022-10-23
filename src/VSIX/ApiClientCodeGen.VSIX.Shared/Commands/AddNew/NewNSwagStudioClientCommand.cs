using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;

namespace Rapicgen.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewNSwagStudioClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0600;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.NSwagStudio;
    }
}