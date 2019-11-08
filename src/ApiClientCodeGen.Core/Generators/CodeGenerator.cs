using System;
using System.IO;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators
{
    public interface ICodeGenerator
    {
        string GenerateCode(IProgressReporter pGenerateProgress);
    }

    public abstract class CodeGenerator : ICodeGenerator
    {
        protected readonly string DefaultNamespace;
        protected readonly string SwaggerFile;
        private readonly IProcessLauncher processLauncher;

        protected CodeGenerator(string swaggerFile, string defaultNamespace, IProcessLauncher processLauncher)
        {
            SwaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            DefaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
        }

        public virtual string GenerateCode(IProgressReporter pGenerateProgress)
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

                processLauncher.Start(command, arguments);
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