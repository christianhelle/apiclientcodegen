using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public interface ICodeGenerator
    {
        string GenerateCode(IVsGeneratorProgress pGenerateProgress);
    }

    public abstract class CodeGenerator : ICodeGenerator
    {
        protected readonly string SwaggerFile;
        protected readonly string DefaultNamespace;

        protected CodeGenerator(string swaggerFile, string defaultNamespace)
        {
            SwaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            DefaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
        }

        public virtual string GenerateCode(IVsGeneratorProgress pGenerateProgress)
        {
            try
            {
                pGenerateProgress.Progress(10);
                var path = Path.GetDirectoryName(SwaggerFile);
                var outputFile = Path.Combine(
                    path ?? throw new InvalidOperationException(),
                    $"{Guid.NewGuid()}.cs");

                var command = GetCommand();
                var arguments = GetArguments(outputFile);
                pGenerateProgress.Progress(30);

                ProcessHelper.StartProcess(command, arguments);
                pGenerateProgress.Progress(80);

                return FileHelper.ReadThenDelete(outputFile);
            }
            finally
            {
                pGenerateProgress.Progress(90);
            }
        }

        protected abstract string GetArguments(string outputFile);
        protected abstract string GetCommand();
    }
}
