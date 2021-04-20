using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag
{
    public class NSwagCSharpOptions 
        : OptionsBase<INSwagOptions, NSwagOptionsPage>, INSwagOptions
    {
        public NSwagCSharpOptions(INSwagOptions options = null)
        {
            try
            {
                if (options == null)
                    options = GetFromDialogPage();

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
                TraceLogger.WriteLine("InjectHttpClient = true");
                TraceLogger.WriteLine("GenerateClientInterfaces = true");
                TraceLogger.WriteLine("GenerateDtoTypes = true");
                TraceLogger.WriteLine("UseBaseUrl = false");
                TraceLogger.WriteLine("ClassStyle = CSharpClassStyle.Poco");
                TraceLogger.WriteLine("UseDocumentTitle = true");

                InjectHttpClient = true;
                GenerateClientInterfaces = true;
                GenerateDtoTypes = true;
                UseBaseUrl = false;
                ClassStyle = CSharpClassStyle.Poco;
                UseDocumentTitle = true;
            }
        }

        public bool InjectHttpClient { get; }
        public bool GenerateClientInterfaces { get; }
        public bool GenerateDtoTypes { get; }
        public bool UseBaseUrl { get; }
        public CSharpClassStyle ClassStyle { get; }
        public bool UseDocumentTitle { get; }
    }
}