using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;

namespace ApiClientCodeGen.VSMac.CustomTools.AutoRest
{
    public interface IAutoRestCodeGeneratorFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher);
    }

    public class AutoRestCodeGeneratorFactory : IAutoRestCodeGeneratorFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher)
            => new AutoRestCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                options,
                processLauncher);
    }
}