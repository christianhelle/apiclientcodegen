using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag
{
    [ExcludeFromCodeCoverage]
    [Guid("0A31911A-4D1B-47CB-8F89-B93731A1FA31")]
    [ComVisible(true)]
    [ProvideObject(typeof(NSwagCSharpCodeGenerator))]
    [CodeGeneratorRegistration(typeof(NSwagCSharpCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.CSharpProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "NSwagCodeGenerator")]
    public class NSwagCSharpCodeGenerator : NSwagCodeGenerator
    {
        public const string Description = "C# NSwag API Client Code Generator";

        public NSwagCSharpCodeGenerator() 
            : base(SupportedLanguage.CSharp)
        {
        }

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cs";
            return 0;
        }
    }
}
