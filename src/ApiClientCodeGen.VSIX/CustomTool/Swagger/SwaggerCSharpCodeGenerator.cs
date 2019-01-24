using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger
{
    [Guid("0A31911A-4D1B-47CB-8F89-B93731A1FA31")]
    [ComVisible(true)]
    [ProvideObject(typeof(SwaggerCSharpCodeGenerator))]
    [CodeGeneratorRegistration(typeof(SwaggerCSharpCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.CSharpProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "NSwagCodeGenerator")]
    public class SwaggerCSharpCodeGenerator : NSwagCodeGenerator
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
