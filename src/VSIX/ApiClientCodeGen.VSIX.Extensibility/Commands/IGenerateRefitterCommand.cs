using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands
{
    public interface IGenerateRefitterCommand
    {
        CommandConfiguration CommandConfiguration { get; }

        Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken);
    }
}