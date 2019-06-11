using System;
using System.Diagnostics;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class NSwagStudioOptions : NSwagCSharpOptions, INSwagStudioOptions
    {
        public NSwagStudioOptions(INSwagStudioOptions options = null)
            : base(options)
        {
            try
            {
                if (options == null)
                    options = (INSwagStudioOptions)VsPackage.Instance.GetDialogPage(typeof(NSwagStudioOptionsPage));

                GenerateResponseClasses = options.GenerateResponseClasses;
                GenerateJsonMethods = options.GenerateJsonMethods;
                RequiredPropertiesMustBeDefined = options.RequiredPropertiesMustBeDefined;
                GenerateDefaultValues = options.GenerateDefaultValues;
                GenerateDataAnnotations = options.GenerateDataAnnotations;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Error reading user options. Reverting to default values");
                Trace.WriteLine("GenerateResponseClasses = true");
                Trace.WriteLine("GenerateJsonMethods = true");
                Trace.WriteLine("RequiredPropertiesMustBeDefined = true");
                Trace.WriteLine("GenerateDefaultValues = true");
                Trace.WriteLine("GenerateDataAnnotations = true");

                GenerateResponseClasses = true;
                GenerateJsonMethods = true;
                RequiredPropertiesMustBeDefined = true;
                GenerateDefaultValues = true;
                GenerateDataAnnotations = true;
            }
        }

        public bool GenerateResponseClasses { get; set; }
        public bool GenerateJsonMethods { get; set; }
        public bool RequiredPropertiesMustBeDefined { get; set; }
        public bool GenerateDefaultValues { get; set; }
        public bool GenerateDataAnnotations { get; set; }
    }
}