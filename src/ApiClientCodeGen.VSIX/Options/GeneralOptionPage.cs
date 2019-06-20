using System.ComponentModel;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using Microsoft.VisualStudio.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class GeneralOptionPage : DialogPage, IGeneralOptions
    {
        public const string Name = "General";

        public GeneralOptionPage()
        {
            JavaPath = PathProvider.GetJavaPath();
            NpmPath = PathProvider.GetNpmPath();
            NSwagPath = PathProvider.GetNSwagPath();
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
        [Description("Full path to nswag.exe")]
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
