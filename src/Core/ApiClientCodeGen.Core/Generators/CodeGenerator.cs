using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators
{
    public interface ICodeGenerator
    {
        string GenerateCode(IProgressReporter? pGenerateProgress);
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

        public virtual string GenerateCode(IProgressReporter? pGenerateProgress)
        {
            try
            {
                pGenerateProgress?.Progress(10);
                var outputFile = FileHelper.CreateRandomFile();

                var command = GetCommand();
                var arguments = GetArguments(outputFile);
                pGenerateProgress?.Progress(30);

                processLauncher.Start(command, arguments, Path.GetDirectoryName(SwaggerFile));
                pGenerateProgress?.Progress(80);

                return FileHelper.ReadThenDelete(outputFile);
            }
            catch (Exception e)
            {
                throw new CodeGeneratorException(GetType().Name, e);
            }
            finally
            {
                pGenerateProgress?.Progress(90);
            }
        }

        protected abstract string GetArguments(string outputFile);
        protected abstract string GetCommand();
    }
}