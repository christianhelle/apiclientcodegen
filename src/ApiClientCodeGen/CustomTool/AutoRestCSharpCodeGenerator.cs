using System;
using System.Runtime.InteropServices;
using ApiClientCodeGen.Core;
using Microsoft.VisualStudio.Shell;

namespace ApiClientCodeGen
{
    [Guid("A2AE3194-DD0B-44FC-B8C4-B40EB2BF6498")]
    [ComVisible(true)]
    [ProvideObject(typeof(AutoRestCSharpCodeGenerator))]
    [CodeGeneratorRegistration(typeof(AutoRestCSharpCodeGenerator),
                              "C# AutoRest API Client Code Generator",
                              Guids.AutoRestCSharpCodeGenerator,
                              GeneratesDesignTimeSource = true,
                              GeneratorRegKeyName = nameof(AutoRestCodeGenerator))]
    public class AutoRestCSharpCodeGenerator : AutoRestCodeGenerator
    {
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
