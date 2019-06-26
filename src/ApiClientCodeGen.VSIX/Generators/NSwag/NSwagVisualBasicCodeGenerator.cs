using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using Microsoft.VisualStudio.Shell.Interop;
using ICSharpCode.CodeConverter.VB;
using ICSharpCode.CodeConverter.Shared;
using System;

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
            var csharp = csharpGenerator.GenerateCode(pGenerateProgress);
            var result = ProjectConversion
                .ConvertText<CSToVBConversion>(csharp, null, defaultNamespace)
                .GetAwaiter()
                .GetResult();

            if (!result.Success)
                throw new Exception(result.GetExceptionsAsString());

            return result.ConvertedCode;
        }
    }
}
