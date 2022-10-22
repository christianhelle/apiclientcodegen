using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Converters;
using Rapicgen.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace Rapicgen.CustomTool.NSwag
{
    [ExcludeFromCodeCoverage]
    [Guid("5EB59F57-9C54-45F1-9301-59CBF65385D0")]
    [ComVisible(true)]
    [ProvideObject(typeof(NSwagVisualBasicCodeGenerator))]
    [CodeGeneratorRegistration(typeof(NSwagVisualBasicCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.VisualBasicProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "NSwagCodeGenerator")]
    public class NSwagVisualBasicCodeGenerator : NSwagCodeGenerator
    {
        public const string Description = "VB.NET NSwag API Client Code Generator";

        public NSwagVisualBasicCodeGenerator() 
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
