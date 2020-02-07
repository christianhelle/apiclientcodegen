using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;

namespace ApiClientCodeGen.CLI.Commands
{
    public interface IAutoRestCodeGeneratorFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            object options,
            IProcessLauncher processLauncher);
    }

    public class AutoRestCodeGeneratorFactory : IAutoRestCodeGeneratorFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            object options,
            IProcessLauncher processLauncher)
            => new AutoRestCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                (IAutoRestOptions)options,
                processLauncher);
    }
}