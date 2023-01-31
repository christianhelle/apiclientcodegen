using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace Rapicgen.CustomTool.Kiota
{
    [ExcludeFromCodeCoverage]
    [Guid("06374E1D-C94A-431B-938D-50A9A7C1D362")]
    [ComVisible(true)]
    [ProvideObject(typeof(KiotaCSharpCodeGenerator))]
    [CodeGeneratorRegistration(typeof(KiotaCSharpCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.CSharpProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "KiotaCodeGenerator")]
    public class KiotaCSharpCodeGenerator : KiotaCodeGenerator
    {
        public const string Description = "C# Kiota Client Code Generator";

        public KiotaCSharpCodeGenerator()
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
