using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class SingleFileCodeGenerator : IVsSingleFileGenerator
    {
        private readonly SupportedLanguage supportedLanguage;
        private readonly ILanguageConverter converter;

        public SupportedCodeGenerator CodeGenerator { get; }

        protected SingleFileCodeGenerator(
            SupportedCodeGenerator supportedCodeGenerator,
            SupportedLanguage supportedLanguage = SupportedLanguage.CSharp,
            ILanguageConverter converter = null)
        {
            this.CodeGenerator = supportedCodeGenerator;
            this.supportedLanguage = supportedLanguage;
            this.converter = converter;
        }

        public abstract int DefaultExtension(out string pbstrDefaultExtension);

        public ICodeGeneratorFactory Factory { get; set; } = new CodeGeneratorFactory();

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
                pGenerateProgress.Progress(5);

                var codeGenerator = Factory.Create(
                    wszDefaultNamespace,
                    bstrInputFileContents,
                    wszInputFilePath,
                    supportedLanguage,
                    CodeGenerator);

                var code = codeGenerator.GenerateCode(new ProgressReporter(pGenerateProgress));
                if (string.IsNullOrWhiteSpace(code))
                {
                    pcbOutput = 0;
                    return 1;
                }

                if (supportedLanguage == SupportedLanguage.VisualBasic && converter != null)
                {
                    Trace.WriteLine(Environment.NewLine);
                    Trace.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Trace.WriteLine("!!! EXPERIMENTAL - Attempting to convert C# code to Visual Basic !!!");
                    Trace.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Trace.WriteLine(Environment.NewLine);

                    code = converter
                        .ConvertAsync(code)
                        .GetAwaiter()
                        .GetResult();
                }

                rgbOutputFileContents[0] = code.ConvertToIntPtr(out pcbOutput);
                pGenerateProgress.Progress(100);
            }
            catch (Exception e)
            {
                pGenerateProgress.GeneratorError(e);
                Trace.WriteLine("Unable to generate code");
                Trace.WriteLine(e);
                throw;
            }

            return 0;
        }
    }
}
