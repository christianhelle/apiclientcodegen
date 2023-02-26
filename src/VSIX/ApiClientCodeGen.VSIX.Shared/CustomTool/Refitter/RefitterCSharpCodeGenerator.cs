using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace Rapicgen.CustomTool.Refitter
{
    [ExcludeFromCodeCoverage]
    [Guid("8CED7707-DCAE-4304-AEAF-27E2508902F0")]
    [ComVisible(true)]
    [ProvideObject(typeof(RefitterCSharpCodeGenerator))]
    [CodeGeneratorRegistration(typeof(RefitterCSharpCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.CSharpProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "RefitterCodeGenerator")]
    public class RefitterCSharpCodeGenerator : RefitterCodeGenerator
    {
        public const string Description = "C# Refit Client Code Generator";

        public RefitterCSharpCodeGenerator()
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
