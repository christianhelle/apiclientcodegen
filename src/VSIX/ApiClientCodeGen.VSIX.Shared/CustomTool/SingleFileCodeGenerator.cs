using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
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

        public ICodeGeneratorFactory Factory { get; set; } = new CodeGeneratorFactory(null);

        public int Generate(
            string wszInputFilePath,
            string bstrInputFileContents,
            string wszDefaultNamespace,
            IntPtr[] rgbOutputFileContents,
            out uint pcbOutput,
            IVsGeneratorProgress pGenerateProgress)
        {
            pcbOutput = 0;
            var progressReporter = new ProgressReporter(pGenerateProgress);
            
            try
            {
                progressReporter.Progress(5);

                var codeGenerator = Factory.Create(
                    wszDefaultNamespace,
                    bstrInputFileContents,
                    wszInputFilePath,
                    supportedLanguage,
                    CodeGenerator);

                var code = codeGenerator.GenerateCode(progressReporter);
                if (string.IsNullOrWhiteSpace(code))
                {
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
                
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine($"Output file size: {pcbOutput}");
                
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("###################################################################");
                Trace.WriteLine("#  Do you find this tool useful?                                  #");
                Trace.WriteLine("#  https://www.buymeacoffee.com/christianhelle                    #");
                Trace.WriteLine("#                                                                 #");
                Trace.WriteLine("#  Does this tool not work or does it lack something you need?    #");
                Trace.WriteLine("#  https://github.com/christianhelle/apiclientcodegen/issues      #");
                Trace.WriteLine("###################################################################");
            }
            catch (NotSupportedException e)
            {
                MessageBox.Show(e.Message, "Not Supported");
                LogException(e, pGenerateProgress, rgbOutputFileContents, out pcbOutput);
            }
            catch (Exception e)
            {
                LogException(e, pGenerateProgress, rgbOutputFileContents, out pcbOutput);
                throw new CustomToolException(GetType().Name, e);
            }
            finally
            {
                progressReporter.Progress(100);
            }

            return 0;
        }

        private static void LogException(
            Exception e,
            IVsGeneratorProgress pGenerateProgress,
            IntPtr[] rgbOutputFileContents,
            out uint pcbOutput)
        {
            Logger.Instance.TrackError(e);
            rgbOutputFileContents[0] = string.Empty.ConvertToIntPtr(out pcbOutput);
            pGenerateProgress.GeneratorError(e);

            Trace.WriteLine("Unable to generate code");
            Trace.WriteLine(e);

            Trace.WriteLine(Environment.NewLine);
            Trace.WriteLine("######################################################################################");
            Trace.WriteLine("# Create a Github Issue at https://github.com/christianhelle/apiclientcodegen/issues #");
            Trace.WriteLine("######################################################################################");
            Trace.WriteLine(Environment.NewLine);
        }
    }
}
