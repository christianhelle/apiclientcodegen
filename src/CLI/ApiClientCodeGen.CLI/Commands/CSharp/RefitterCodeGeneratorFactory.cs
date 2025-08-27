using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Refitter;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.Refitter;

namespace Rapicgen.CLI.Commands.CSharp;

public interface IRefitterCodeGeneratorFactory
{
    ICodeGenerator Create(
        string swaggerFile,
        string defaultNamespace,
        IProcessLauncher processLauncher,
        IDependencyInstaller dependencyInstaller,
        IRefitterOptions options);
}

public class RefitterCodeGeneratorFactory : IRefitterCodeGeneratorFactory
{
    public ICodeGenerator Create(
        string swaggerFile,
        string defaultNamespace,
        IProcessLauncher processLauncher,
        IDependencyInstaller dependencyInstaller,
        IRefitterOptions options) =>
        new RefitterCodeGenerator(
            swaggerFile,
            defaultNamespace,
            processLauncher,
            dependencyInstaller,
            options);
}