using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace Rapicgen.CustomTool.Swagger
{
    [ExcludeFromCodeCoverage]
    [Guid("DBE9FF25-8BA0-412D-A87B-712AFF162451")]
    [ComVisible(true)]
    [ProvideObject(typeof(SwaggerCSharpCodeGenerator))]
    [CodeGeneratorRegistration(typeof(SwaggerCSharpCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.CSharpProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "SwaggerCodeGenerator")]
    public class SwaggerCSharpCodeGenerator : SwaggerCodeGenerator
    {
        public const string Description = "C# Swagger API Client Code Generator";

        public SwaggerCSharpCodeGenerator() 
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
