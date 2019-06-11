using System.ComponentModel;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class NSwagStudioOptionsPage : NSwagOptionsPage, INSwagStudioOptions
    {
        public new const string Name = "NSwag Studio";
        
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
        [DisplayName("Generate Default Values")]
        [Description("Set this to TRUE to generate data annotations")]
        public bool GenerateDataAnnotations { get; set; } = true;
    }
}
