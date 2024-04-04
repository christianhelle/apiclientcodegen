using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Rapicgen.Core.Options.General;
using Rapicgen.Windows;
using Microsoft.VisualStudio.Shell;
using Rapicgen.Core.External;

namespace Rapicgen.Options.General
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
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
        public string SwaggerCodegenPath { get; set; } = null!;

        [Category("File Paths")]
        [DisplayName("OpenAPI Generator Path")]
        [Description("Full path OpenAPI Generator JAR file")]
        public string OpenApiGeneratorPath { get; set; } = null!;

        [Category("NuGet Options")]
        [DisplayName("Install Required Packages")]
        [Description("Automatically install required NuGet packages")]
        public bool? InstallMissingPackages { get; set; } = true;

        protected override IWin32Window Window
            => new GeneralOptionsPageCustom(this);
    }
}
