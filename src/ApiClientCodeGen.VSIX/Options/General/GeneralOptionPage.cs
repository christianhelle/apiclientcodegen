using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using Microsoft.VisualStudio.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General
{
    [ExcludeFromCodeCoverage]
    public class GeneralOptionPage : DialogPage, IGeneralOptions
    {
        public const string Name = "General";

        public GeneralOptionPage()
        {
            JavaPath = PathProvider.GetJavaPath();
            NpmPath = PathProvider.GetNpmPath();
            NSwagPath = PathProvider.GetNSwagStudioPath();
        }

        [Category("File Paths")]
        [DisplayName("Java Path")]
        [Description("Full path to java.exe. Leave empty to get path from JAVA_HOME")]
        public string JavaPath { get; set; }

        [Category("File Paths")]
        [DisplayName("NPM Path")]
        [Description("Full path to npm.cmd")]
        public string NpmPath { get; set; }

        [Category("File Paths")]
        [DisplayName("NSwag Path")]
        [Description("Full path to NSwag.exe (Installs from NPM if not found)")]
        public string NSwagPath { get; set; }
        
        [Category("File Paths")]
        [DisplayName("Swagger Codegen CLI Path")]
        [Description("Full path to Swagger Codegen JAR file")]
        public string SwaggerCodegenPath { get; set; }
        
        [Category("File Paths")]
        [DisplayName("OpenAPI Generator Path")]
        [Description("Full path OpenAPI Generator JAR file")]
        public string OpenApiGeneratorPath { get; set; }

        protected override IWin32Window Window
            => new GeneralOptionsPageCustom(this);
    }
}
