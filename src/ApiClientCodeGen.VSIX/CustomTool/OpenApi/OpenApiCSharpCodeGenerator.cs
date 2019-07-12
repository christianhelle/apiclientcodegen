using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.OpenApi
{
    [ExcludeFromCodeCoverage]
    [Guid("86F43699-81FF-4CE6-BFC1-7CB50499A3DE")]
    [ComVisible(true)]
    [ProvideObject(typeof(OpenApiCSharpCodeGenerator))]
    [CodeGeneratorRegistration(typeof(OpenApiCSharpCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.CSharpProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "OpenApiCodeGenerator")]
    public class OpenApiCSharpCodeGenerator : OpenApiCodeGenerator
    {
        public const string Description = "C# OpenAPI Client Code Generator";

        public OpenApiCSharpCodeGenerator() 
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
