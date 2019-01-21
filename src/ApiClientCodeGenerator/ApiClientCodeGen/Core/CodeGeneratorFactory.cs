using System;
using System.Reflection;

namespace ApiClientCodeGen.Core
{
    public class CodeGeneratorFactory
    {
        public ICodeGenerator Create(
            string className,
            string defaultNamespace,
            string inputFileContents,
            string inputFilePath,
            SupportedLanguage language)
        {
            throw new NotImplementedException();
        }
    }
}
