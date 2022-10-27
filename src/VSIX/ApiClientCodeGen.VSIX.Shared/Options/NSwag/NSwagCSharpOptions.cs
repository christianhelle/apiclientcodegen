using System;
using System.Diagnostics;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.NSwag;
using NJsonSchema.CodeGeneration.CSharp;

namespace Rapicgen.Options.NSwag
{
    public class NSwagCSharpOptions 
        : OptionsBase<INSwagOptions, NSwagOptionsPage>, INSwagOptions
    {
        public NSwagCSharpOptions(INSwagOptions? options = null)
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
                ParameterDateTimeFormat = options.ParameterDateTimeFormat;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                
                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Error reading user options. Reverting to default values");
                Trace.WriteLine("InjectHttpClient = true");
                Trace.WriteLine("GenerateClientInterfaces = true");
                Trace.WriteLine("GenerateDtoTypes = true");
                Trace.WriteLine("UseBaseUrl = false");
                Trace.WriteLine("ClassStyle = CSharpClassStyle.Poco");
                Trace.WriteLine("UseDocumentTitle = true");
                Trace.WriteLine("ParameterDateTimeFormat = s");

                InjectHttpClient = true;
                GenerateClientInterfaces = true;
                GenerateDtoTypes = true;
                UseBaseUrl = false;
                ClassStyle = CSharpClassStyle.Poco;
                UseDocumentTitle = true;
                ParameterDateTimeFormat = "s";
            }
        }

        public bool InjectHttpClient { get; }
        public bool GenerateClientInterfaces { get; }
        public bool GenerateDtoTypes { get; }
        public bool UseBaseUrl { get; }
        public CSharpClassStyle ClassStyle { get; }
        public bool UseDocumentTitle { get; }
        public string ParameterDateTimeFormat { get; }
    }
}