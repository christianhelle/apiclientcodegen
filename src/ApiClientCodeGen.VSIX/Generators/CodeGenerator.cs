using System;
using System.IO;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public interface ICodeGenerator
    {
        string GenerateCode(IVsGeneratorProgress pGenerateProgress);
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

        public string GenerateCode(IVsGeneratorProgress pGenerateProgress)
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
