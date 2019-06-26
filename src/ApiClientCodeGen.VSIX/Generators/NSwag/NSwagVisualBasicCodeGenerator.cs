using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using ICSharpCode.CodeConverter;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag
{
    public class NSwagVisualBasicCodeGenerator : ICodeGenerator
    {
        private readonly NSwagCSharpCodeGenerator csharpGenerator;
        private readonly string defaultNamespace;

        public NSwagVisualBasicCodeGenerator(
            string swaggerFile, 
            string defaultNamespace, 
            INSwagOptions options)
        {
            csharpGenerator = new NSwagCSharpCodeGenerator(swaggerFile, defaultNamespace, options);
            this.defaultNamespace = defaultNamespace;
        }

        public string GenerateCode(IVsGeneratorProgress pGenerateProgress)
        {
            try
            {
                var csharp = csharpGenerator.GenerateCode(pGenerateProgress);
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
