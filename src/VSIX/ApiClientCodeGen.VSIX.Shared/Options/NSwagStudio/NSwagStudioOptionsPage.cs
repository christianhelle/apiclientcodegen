using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.NSwagStudio;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace Rapicgen.Options.NSwagStudio
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public class NSwagStudioOptionsPage : DialogPage, INSwagStudioOptions
    {
        public const string Name = "NSwag Studio";

        [Category(Name)]
        [DisplayName("Inject HttpClient")]
        [Description("Set this to TRUE to generate the constructor that accepts HttpClient")]
        public bool InjectHttpClient { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate Interfaces")]
        [Description("Set this to TRUE to generate client interfaces")]
        public bool GenerateClientInterfaces { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate DTO types")]
        [Description("Set this to TRUE to generate DTO types")]
        public bool GenerateDtoTypes { get; set; } = true;

        [Category(Name)]
        [DisplayName("Use Base URL")]
        [Description("Set this to TRUE to include a base URL for every HTTP request")]
        public bool UseBaseUrl { get; set; } = false;

        [Category(Name)]
        [DisplayName("C# Class Style")]
        [Description("POCO (Plain Old C# Objects), Inpc (Implements INotifyPropertyChanged), Prism (Prism base class), Records (readonly POCO)")]
        public CSharpClassStyle ClassStyle { get; set; }

        [Category(Name)]
        [DisplayName("Document title as class name")]
        [Description("Set this to TRUE to use the OpenAPI Document Info Title as the generated class name. Set this to FALSE to use the filename")]
        public bool UseDocumentTitle { get; set; } = true;

        [Category(Name)]
        [DisplayName("Parameter DateTime format")]
        [Description("Specifies the format for DateTime type method parameters (default: s)")]
        public string ParameterDateTimeFormat { get; set; } = "s";

        [Category(Name)]
        [DisplayName("Generate Response Classes")]
        [Description("Set this to TRUE to generate response classes")]
        public bool GenerateResponseClasses { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate JSON methods")]
        [Description("Set this to TRUE to generate JSON methods")]
        public bool GenerateJsonMethods { get; set; } = true;

        [Category(Name)]
        [DisplayName("Required properties must be defined")]
        [Description("Set this to TRUE if required properties must be defined")]
        public bool RequiredPropertiesMustBeDefined { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate Default Values")]
        [Description("Set this to TRUE to generate default values")]
        public bool GenerateDefaultValues { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate Data Annotations")]
        [Description("Set this to TRUE to generate data annotations")]
        public bool GenerateDataAnnotations { get; set; } = true;
    }
}
