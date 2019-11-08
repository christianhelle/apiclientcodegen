using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest
{
    [ExcludeFromCodeCoverage]
    [Guid("EB8E6CE3-C2AC-46F3-A397-336618B567D2")]
    [ComVisible(true)]
    [ProvideObject(typeof(AutoRestVisualBasicCodeGenerator))]
    [CodeGeneratorRegistration(typeof(AutoRestVisualBasicCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.VisualBasicProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "AutoRestCodeGenerator")]
    public class AutoRestVisualBasicCodeGenerator : AutoRestCodeGenerator
    {
        public const string Description = "VB.NET AutoRest API Client Code Generator";

        public AutoRestVisualBasicCodeGenerator()
            : base(SupportedLanguage.VisualBasic, new CSharpToVisualBasicLanguageConverter())
        {
        }

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".vb";
            return 0;
        }
    }
}
