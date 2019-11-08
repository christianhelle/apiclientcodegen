using System;
using System.Diagnostics;
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
                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Error reading user options. Reverting to default values");
                Trace.WriteLine("GenerateResponseClasses = true");
                Trace.WriteLine("GenerateJsonMethods = true");
                Trace.WriteLine("RequiredPropertiesMustBeDefined = true");
                Trace.WriteLine("GenerateDefaultValues = true");
                Trace.WriteLine("GenerateDataAnnotations = true");
                Trace.WriteLine("InjectHttpClient = true");
                Trace.WriteLine("GenerateClientInterfaces = true");
                Trace.WriteLine("GenerateDtoTypes = true");
                Trace.WriteLine("UseBaseUrl = false");
                Trace.WriteLine("ClassStyle = CSharpClassStyle.Poco");
                Trace.WriteLine("UseDocumentTitle = true");

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