using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Rapicgen.Core;
using Rapicgen.Core.Converters;
using Rapicgen.Core.Exceptions;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Extensions;
using Rapicgen.Generators;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Rapicgen.CustomTool
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class SingleFileCodeGenerator : IVsSingleFileGenerator
    {
        private readonly SupportedLanguage supportedLanguage;
        private readonly ILanguageConverter? converter;

        public SupportedCodeGenerator CodeGenerator { get; }

        protected SingleFileCodeGenerator(
            SupportedCodeGenerator supportedCodeGenerator,
            SupportedLanguage supportedLanguage = SupportedLanguage.CSharp,
            ILanguageConverter? converter = null)
        {
            this.CodeGenerator = supportedCodeGenerator;
            this.supportedLanguage = supportedLanguage;
            this.converter = converter;
        }

        public abstract int DefaultExtension(out string pbstrDefaultExtension);

        public ICodeGeneratorFactory Factory { get; set; } = new CodeGeneratorFactory();

        [SuppressMessage(
            "Usage", "VSTHRD108:Assert thread affinity unconditionally",
            Justification = "ThrowIfNotOnUIThread() causes unit tests to fail")]
        public int Generate(
            string wszInputFilePath,
            string bstrInputFileContents,
            string wszDefaultNamespace,
            IntPtr[] rgbOutputFileContents,
            out uint pcbOutput,
            IVsGeneratorProgress pGenerateProgress)
        {
            if (!TestingUtility.IsRunningFromUnitTest)
                ThreadHelper.ThrowIfNotOnUIThread();

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
                    Logger.Instance.WriteLine(Environment.NewLine);
                    Logger.Instance.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Logger.Instance.WriteLine("!!! EXPERIMENTAL - Attempting to convert C# code to Visual Basic !!!");
                    Logger.Instance.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Logger.Instance.WriteLine(Environment.NewLine);

                    code = converter
                        .ConvertAsync(code)
                        .GetAwaiter()
                        .GetResult();
                }

                rgbOutputFileContents[0] = code.ConvertToIntPtr(out pcbOutput);

                Logger.Instance.WriteLine(Environment.NewLine);
                Logger.Instance.WriteLine($"Output file size: {pcbOutput}");

                Logger.Instance.WriteLine(Environment.NewLine);
                Logger.Instance.WriteLine("#######################################################################################");
                Logger.Instance.WriteLine("#                                                                                     #");
                Logger.Instance.WriteLine("#  I would be very grateful for a rating or review on the Visual Studio Marketplace   #");
                Logger.Instance.WriteLine("#  - Visual Studio 2022 - https://bit.ly/rapicgen-vs2022                              #");
                Logger.Instance.WriteLine("#  - Visual Studio 2019 - https://bit.ly/rapicgen-vs2019                              #");
                Logger.Instance.WriteLine("#  - Visual Studio 2017 - https://bit.ly/rapicgen-vs2017                              #");
                Logger.Instance.WriteLine("#                                                                                     #");
                Logger.Instance.WriteLine("#  Does this tool not work or does it lack something you need?                        #");
                Logger.Instance.WriteLine("#  https://github.com/christianhelle/apiclientcodegen/issues                          #");
                Logger.Instance.WriteLine("#                                                                                     #");
                Logger.Instance.WriteLine("#  Are you feeling generous? Do you find this tool useful?                            #");
                Logger.Instance.WriteLine("#  https://www.buymeacoffee.com/christianhelle                                        #");
                Logger.Instance.WriteLine("#                                                                                     #");
                Logger.Instance.WriteLine("#######################################################################################");
            }
            catch (NotSupportedException e)
            {
                MessageBox.Show(e.Message, @"Not Supported");
                LogException(e, pGenerateProgress, rgbOutputFileContents, out pcbOutput);
            }
            catch (MissingJavaRuntimeException e)
            {
                MessageBox.Show(e.Message, @"Unable to find Java executable");
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

        [SuppressMessage(
            "Usage", "VSTHRD108:Assert thread affinity unconditionally",
            Justification = "ThrowIfNotOnUIThread() causes unit tests to fail")]
        private static void LogException(
            Exception e,
            IVsGeneratorProgress pGenerateProgress,
            IntPtr[] rgbOutputFileContents,
            out uint pcbOutput)
        {
            if (!TestingUtility.IsRunningFromUnitTest)
                ThreadHelper.ThrowIfNotOnUIThread();

            Logger.Instance.TrackError(e);
            rgbOutputFileContents[0] = string.Empty.ConvertToIntPtr(out pcbOutput);
            pGenerateProgress?.GeneratorError(e);

            Logger.Instance.WriteLine("Unable to generate code");
            

            Logger.Instance.WriteLine(Environment.NewLine);
            Logger.Instance.WriteLine("######################################################################################");
            Logger.Instance.WriteLine("# Create a Github Issue at https://github.com/christianhelle/apiclientcodegen/issues #");
            Logger.Instance.WriteLine("######################################################################################");
            Logger.Instance.WriteLine(Environment.NewLine);
        }
    }
}
