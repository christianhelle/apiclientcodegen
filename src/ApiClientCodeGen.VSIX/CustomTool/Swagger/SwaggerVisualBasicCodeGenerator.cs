using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger
{
    [ExcludeFromCodeCoverage]
    [Guid("CE6638E5-E6E3-4EF5-90D7-E80DBA61C933")]
    [ComVisible(true)]
    [ProvideObject(typeof(SwaggerVisualBasicCodeGenerator))]
    [CodeGeneratorRegistration(typeof(SwaggerVisualBasicCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.VisualBasicProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "SwaggerCodeGenerator")]
    public class SwaggerVisualBasicCodeGenerator : SwaggerCodeGenerator
    {
        public const string Description = "VB.NET Swagger API Client Code Generator";

        public SwaggerVisualBasicCodeGenerator() 
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
