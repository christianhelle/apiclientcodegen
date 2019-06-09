using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public interface INSwagOption
    {
        bool InjectHttpClient { get; set; }
        bool GenerateClientInterfaces { get; set; }
        bool GenerateDtoTypes { get; set; }
        bool UseBaseUrl { get; set; }
        CSharpClassStyle ClassStyle { get; set; }
    }

    public class NSwagOptionsPage : DialogPage, INSwagOption
    {
        public const string Name = "NSwag";

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
