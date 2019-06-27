using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ICSharpCode.CodeConverter;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.AutoRest
{
    public class AutoRestVisualBasicCodeGenerator : ICodeGenerator
    {
        private readonly ICodeGenerator generator;

        public AutoRestVisualBasicCodeGenerator(string swaggerFile, string defaultNamespace)
        {
            generator = new AutoRestCSharpCodeGenerator(swaggerFile, defaultNamespace);
        }

        public string GenerateCode(IVsGeneratorProgress pGenerateProgress)
        {
            try
            {
                var csharp = generator.GenerateCode(pGenerateProgress);
                var result = CodeConverter
                    .Convert(new CodeWithOptions(csharp))
                    .GetAwaiter()
                    .GetResult();

                if (!result.Success)
                    throw new Exception(result.GetExceptionsAsString());

                return result.ConvertedCode;
            }
            finally
            {
                pGenerateProgress?.Progress(95);
            }
        }
    }
}
