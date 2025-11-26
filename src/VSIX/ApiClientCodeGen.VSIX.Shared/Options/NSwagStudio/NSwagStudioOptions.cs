using System;
using System.Diagnostics;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.NSwagStudio;

namespace Rapicgen.Options.NSwagStudio
{
    public class NSwagStudioOptions
        : OptionsBase<INSwagStudioOptions, NSwagStudioOptionsPage>, INSwagStudioOptions
    {
        public NSwagStudioOptions(INSwagStudioOptions? options = null)
        {
            try
            {
                options ??= GetFromDialogPage();
                GenerateResponseClasses = options.GenerateResponseClasses;
                GenerateJsonMethods = options.GenerateJsonMethods;
                RequiredPropertiesMustBeDefined = options.RequiredPropertiesMustBeDefined;
                GenerateDefaultValues = options.GenerateDefaultValues;
                GenerateDataAnnotations = options.GenerateDataAnnotations;

                InjectHttpClient = options.InjectHttpClient;
                GenerateClientInterfaces = options.GenerateClientInterfaces;
                GenerateDtoTypes = options.GenerateDtoTypes;
                UseBaseUrl = options.UseBaseUrl;
                ClassStyle = options.ClassStyle;
                UseDocumentTitle = options.UseDocumentTitle;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                
                
                Logger.Instance.WriteLine(Environment.NewLine);
                Logger.Instance.WriteLine("Error reading user options. Reverting to default values");
                Logger.Instance.WriteLine("GenerateResponseClasses = true");
                Logger.Instance.WriteLine("GenerateJsonMethods = true");
                Logger.Instance.WriteLine("RequiredPropertiesMustBeDefined = true");
                Logger.Instance.WriteLine("GenerateDefaultValues = true");
                Logger.Instance.WriteLine("GenerateDataAnnotations = true");
                Logger.Instance.WriteLine("InjectHttpClient = true");
                Logger.Instance.WriteLine("GenerateClientInterfaces = true");
                Logger.Instance.WriteLine("GenerateDtoTypes = true");
                Logger.Instance.WriteLine("UseBaseUrl = false");
                Logger.Instance.WriteLine("ClassStyle = CSharpClassStyle.Poco");
                Logger.Instance.WriteLine("UseDocumentTitle = true");

                GenerateResponseClasses = true;
                GenerateJsonMethods = true;
                RequiredPropertiesMustBeDefined = true;
                GenerateDefaultValues = true;
                GenerateDataAnnotations = true;

                InjectHttpClient = true;
                GenerateClientInterfaces = true;
                GenerateDtoTypes = true;
                UseBaseUrl = false;
                ClassStyle = CSharpClassStyle.Poco;
                UseDocumentTitle = true;
            }
        }

        public bool GenerateResponseClasses { get; set; }
        public bool GenerateJsonMethods { get; set; }
        public bool RequiredPropertiesMustBeDefined { get; set; }
        public bool GenerateDefaultValues { get; set; }
        public bool GenerateDataAnnotations { get; set; }

        public bool InjectHttpClient { get; }
        public bool GenerateClientInterfaces { get; }
        public bool GenerateDtoTypes { get; }
        public bool UseBaseUrl { get; }
        public CSharpClassStyle ClassStyle { get; }
        public bool UseDocumentTitle { get; }
        public string ParameterDateTimeFormat { get; }
    }
}