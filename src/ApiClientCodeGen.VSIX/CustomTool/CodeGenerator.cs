using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool
{
    [ComVisible(true)]
    public abstract class SingleFileCodeGenerator : IVsSingleFileGenerator
    {
        private readonly SupportedCodeGenerator supportedCodeGenerator;
        private readonly SupportedLanguage supportedLanguage;

        protected SingleFileCodeGenerator(
            SupportedCodeGenerator supportedCodeGenerator,
            SupportedLanguage supportedLanguage = SupportedLanguage.CSharp)
        {
            this.supportedCodeGenerator = supportedCodeGenerator;
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
                    wszDefaultNamespace,
                    bstrInputFileContents,
                    wszInputFilePath,
                    supportedLanguage,
                    supportedCodeGenerator);

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
