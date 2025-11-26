using System;
using System.Diagnostics;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.NSwag;

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

                
                Logger.Instance.WriteLine(Environment.NewLine);
                Logger.Instance.WriteLine("Error reading user options. Reverting to default values");
                Logger.Instance.WriteLine("InjectHttpClient = true");
                Logger.Instance.WriteLine("GenerateClientInterfaces = true");
                Logger.Instance.WriteLine("GenerateDtoTypes = true");
                Logger.Instance.WriteLine("UseBaseUrl = false");
                Logger.Instance.WriteLine("ClassStyle = CSharpClassStyle.Poco");
                Logger.Instance.WriteLine("UseDocumentTitle = true");
                Logger.Instance.WriteLine("ParameterDateTimeFormat = s");

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