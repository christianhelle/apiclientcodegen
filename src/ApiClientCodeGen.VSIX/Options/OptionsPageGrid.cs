using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public interface IGeneralOptions
    {
        string JavaPath { get; set; }
        string NpmPath { get; set; }
        string NSwagPath { get; set; }
    }

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
        [DisplayName("Custom Java Path")]
        [Description("Custom full path to java.exe. Leave empty to get path from JAVA_HOME")]
        public string JavaPath { get; set; }

        [Category("File Paths")]
        [DisplayName("NPM Path")]
        [Description("Full path to npm.cmd")]
        public string NpmPath { get; set; }

        [Category("File Paths")]
        [DisplayName("NSwag Path")]
        [Description("Full path to nswag.exe")]
        public string NSwagPath { get; set; }
    }
}
