﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Converters;
using Rapicgen.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace Rapicgen.CustomTool.OpenApi
{
    [ExcludeFromCodeCoverage]
    [Guid("551706C0-F37F-4938-9DF8-9D3B2CBF452A")]
    [ComVisible(true)]
    [ProvideObject(typeof(OpenApiVisualBasicCodeGenerator))]
    [CodeGeneratorRegistration(typeof(OpenApiVisualBasicCodeGenerator),
                              Description,
                              ProvideCodeGeneratorAttribute.VisualBasicProjectGuid,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = "OpenApiCodeGenerator")]
    public class OpenApiVisualBasicCodeGenerator : OpenApiCodeGenerator
    {
        public const string Description = "VB.NET OpenAPI Client Code Generator";

        public OpenApiVisualBasicCodeGenerator() 
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
