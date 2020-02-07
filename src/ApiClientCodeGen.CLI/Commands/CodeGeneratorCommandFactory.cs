using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;

namespace ApiClientCodeGen.CLI.Commands
{
    public interface ICodeGeneratorCommandFactory
    {
        ICodeGenerator Create<T>(
            string swaggerFile, 
            string defaultNamespace, 
            object options, 
            IProcessLauncher processLauncher)
            where T : CodeGeneratorCommand;
    }

    public class CodeGeneratorCommandFactory : ICodeGeneratorCommandFactory
    {
        public ICodeGenerator Create<T>(
            string swaggerFile, 
            string defaultNamespace, 
            object options, 
            IProcessLauncher processLauncher)
            where T : CodeGeneratorCommand
        {
            if (typeof(T) == typeof(AutoRestCommand))
                return new AutoRestCSharpCodeGenerator(
                    swaggerFile,
                    defaultNamespace,
                    (IAutoRestOptions) options,
                    processLauncher);

            throw new NotSupportedException();
        }
    }
}