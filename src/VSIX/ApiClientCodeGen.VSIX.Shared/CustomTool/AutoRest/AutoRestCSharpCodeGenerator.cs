using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest
{
    [ExcludeFromCodeCoverage]
    [Guid("A2AE3194-DD0B-44FC-B8C4-B40EB2BF6498")]
    [ComVisible(true)]
    [ProvideObject(typeof(AutoRestCSharpCodeGenerator))]
    [CodeGeneratorRegistration(typeof(AutoRestCSharpCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.CSharpProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "AutoRestCodeGenerator")]
    public class AutoRestCSharpCodeGenerator : AutoRestCodeGenerator
    {
        public const string Description = "C# AutoRest API Client Code Generator";

        public AutoRestCSharpCodeGenerator() 
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
