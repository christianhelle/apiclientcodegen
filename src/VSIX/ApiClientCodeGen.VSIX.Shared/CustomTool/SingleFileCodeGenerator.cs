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
using Microsoft.VisualStudio.OLE.Interop;

namespace Rapicgen.CustomTool
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class SingleFileCodeGenerator : IVsSingleFileGenerator, IObjectWithSite
    {
        private readonly SupportedLanguage supportedLanguage;
        private readonly ILanguageConverter? converter;
        private object? site;

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

                // Force output file timestamp to be older than input to ensure VS detects changes next time
                EnsureOutputWillRegenerateOnNextChange(wszInputFilePath);

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
                MessageBox.Show(e.Message, @"Not Supported", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(e, pGenerateProgress, rgbOutputFileContents, out pcbOutput);
            }
            catch (MissingJavaRuntimeException e)
            {
                MessageBox.Show(@"Unable to find java.exe", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// Ensures that the next modification to the input file will trigger regeneration.
        /// This works around Visual Studio's timestamp-based change detection by ensuring
        /// the output file timestamp doesn't block future regenerations.
        /// </summary>
        private static void EnsureOutputWillRegenerateOnNextChange(string inputFilePath)
        {
            try
            {
                // Get the output file path by looking for the generated file
                // VS creates it with the same base name but different extension
                var outputFilePath = GetOutputFilePath(inputFilePath);
                
                if (!string.IsNullOrEmpty(outputFilePath) && 
                    System.IO.File.Exists(outputFilePath) && 
                    System.IO.File.Exists(inputFilePath))
                {
                    // Set output file timestamp to be slightly older than input
                    // This ensures VS will detect the next input file change
                    var inputTime = System.IO.File.GetLastWriteTime(inputFilePath);
                    var outputTime = inputTime.AddSeconds(-1);
                    System.IO.File.SetLastWriteTime(outputFilePath, outputTime);
                    
                    Logger.Instance.WriteLine($"Updated output file timestamp to ensure future regeneration");
                }
            }
            catch (Exception ex)
            {
                // Don't fail generation if timestamp adjustment fails
                Logger.Instance.WriteLine($"Warning: Could not adjust output timestamp: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the output file path from the input file path.
        /// This attempts to find the corresponding .cs or .vb file.
        /// </summary>
        private static string? GetOutputFilePath(string inputFilePath)
        {
            try
            {
                var directory = System.IO.Path.GetDirectoryName(inputFilePath);
                var fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(inputFilePath);
                
                if (string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(fileNameWithoutExt))
                    return null;

                // Check for common output extensions
                var possibleExtensions = new[] { ".cs", ".vb" };
                foreach (var ext in possibleExtensions)
                {
                    var possiblePath = System.IO.Path.Combine(directory, fileNameWithoutExt + ext);
                    if (System.IO.File.Exists(possiblePath))
                        return possiblePath;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public void GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (site == null)
            {
                ppvSite = IntPtr.Zero;
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);
                return;
            }

            var pUnknownPointer = Marshal.GetIUnknownForObject(site);
            try
            {
                Marshal.QueryInterface(pUnknownPointer, ref riid, out ppvSite);
                if (ppvSite == IntPtr.Zero)
                {
                    Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);
                }
            }
            finally
            {
                if (pUnknownPointer != IntPtr.Zero)
                {
                    Marshal.Release(pUnknownPointer);
                }
            }
        }

        public void SetSite(object pUnkSite)
        {
            site = pUnkSite;
        }
    }
}
