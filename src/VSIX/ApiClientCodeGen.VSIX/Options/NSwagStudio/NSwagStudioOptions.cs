using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwagStudio
{
    public class NSwagStudioOptions
        : OptionsBase<INSwagStudioOptions, NSwagStudioOptionsPage>, INSwagStudioOptions
    {
        public NSwagStudioOptions(INSwagStudioOptions options = null)
        {
            try
            {
                if (options == null)
                    options = GetFromDialogPage();

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
                
                TraceLogger.WriteLine(Environment.NewLine);
                TraceLogger.WriteLine("Error reading user options. Reverting to default values");
                TraceLogger.WriteLine("GenerateResponseClasses = true");
                TraceLogger.WriteLine("GenerateJsonMethods = true");
                TraceLogger.WriteLine("RequiredPropertiesMustBeDefined = true");
                TraceLogger.WriteLine("GenerateDefaultValues = true");
                TraceLogger.WriteLine("GenerateDataAnnotations = true");
                TraceLogger.WriteLine("InjectHttpClient = true");
                TraceLogger.WriteLine("GenerateClientInterfaces = true");
                TraceLogger.WriteLine("GenerateDtoTypes = true");
                TraceLogger.WriteLine("UseBaseUrl = false");
                TraceLogger.WriteLine("ClassStyle = CSharpClassStyle.Poco");
                TraceLogger.WriteLine("UseDocumentTitle = true");

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
    }
}