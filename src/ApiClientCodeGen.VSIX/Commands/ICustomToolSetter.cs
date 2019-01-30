using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands
{
    public interface ICustomToolSetter
    {
        Task InitializeAsync(
            AsyncPackage package,
            CancellationToken cancellationToken);
    }
}