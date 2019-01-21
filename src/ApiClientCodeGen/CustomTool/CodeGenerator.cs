using System;
using System.Reflection;
using System.Windows.Forms;
using ApiClientCodeGen.Core;
using ApiClientCodeGen.Extensions;
using Microsoft.VisualStudio.Shell.Interop;

namespace ApiClientCodeGen
{
    public abstract class CodeGenerator : IVsSingleFileGenerator
    {
        private readonly SupportedLanguage supportedLanguage;

        protected CodeGenerator(SupportedLanguage supportedLanguage = SupportedLanguage.CSharp)
        {
            this.supportedLanguage = supportedLanguage;
        }

        public abstract int DefaultExtension(out string pbstrDefaultExtension);

        public int Generate(
            string wszInputFilePath,
            string bstrInputFileContents,
            string wszDefaultNamespace,
            IntPtr[] rgbOutputFileContents,
            out uint pcbOutput,
            IVsGeneratorProgress pGenerateProgress)
        {
            try
            {
                var className = ClassNameExtractor.GetClassName(wszInputFilePath);
                var factory = new CodeGeneratorFactory();
                
                var codeGenerator = factory.Create(
                    className, 
                    wszDefaultNamespace, 
                    bstrInputFileContents,
                    wszInputFilePath,
                    supportedLanguage);

                var code = codeGenerator.GenerateCode();
                rgbOutputFileContents[0] = code.ConvertToIntPtr(out pcbOutput);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Unable to generate code");
                throw;
            }

            return 0;
        }
    }
}
