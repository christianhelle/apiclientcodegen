using System;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public interface ICodeGenerator
    {
        string GenerateCode();
    }

    public abstract class CodeGenerator : ICodeGenerator
    {
        protected readonly string swaggerFile;
        protected readonly string defaultNamespace;

        protected CodeGenerator(string swaggerFile, string defaultNamespace)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
        }

        public string GenerateCode()
        {
            var path = Path.GetDirectoryName(swaggerFile);
            var outputFile = Path.Combine(path, "TempApiClient.cs");

            var command = GetCommand();
            var arguments = GetArguments(outputFile);

            ProcessHelper.StartProcess(command, arguments);
            return FileHelper.ReadThenDelete(outputFile);
        }

        protected abstract string GetArguments(string outputFile);
        protected abstract string GetCommand();
    }
}
