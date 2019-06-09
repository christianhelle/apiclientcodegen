using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class OptionPageGrid : DialogPage
    {
        public OptionPageGrid()
        {
            JavaPath = PathProvider.GetJavaPath();
            NpmPath = PathProvider.GetNpmPath();
            NSwagPath = PathProvider.GetNSwagPath();
        }

        #region File Paths
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
        #endregion

        #region NSwag
        [Category("NSwag")]
        [DisplayName("Inject HttpClient")]
        [Description("Set this to TRUE to generate the constructor that accepts HttpClient")]
        public bool InjectHttpClient { get; set; } = true;

        [Category("NSwag")]
        [DisplayName("Generate Interfaces")]
        [Description("Set this to TRUE to generate client interfaces")]
        public bool GenerateClientInterfaces { get; set; } = true;

        [Category("NSwag")]
        [DisplayName("Generate DTO types")]
        [Description("Set this to TRUE to generate DTO types")]
        public bool GenerateDtoTypes { get; set; } = true;

        [Category("NSwag")]
        [DisplayName("Use Base URL")]
        [Description("Set this to TRUE to include a base URL for every HTTP request")]
        public bool UseBaseUrl { get; set; } = false;

        [Category("NSwag")]
        [DisplayName("C# Class Style")]
        [Description("POCO (Plain Old C# Objects), Inpc (Implements INotifyPropertyChanged), Prism (Prism base class), Records (readonly POCO)")]
        public CSharpClassStyle ClassStyle { get; set; } 
        #endregion
    }
}
