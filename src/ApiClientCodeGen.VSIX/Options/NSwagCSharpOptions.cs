using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class NSwagCSharpOptions : INSwagOptions
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
                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Error reading user options. Reverting to default values");
                Trace.WriteLine("InjectHttpClient = true");
                Trace.WriteLine("GenerateClientInterfaces = true");
                Trace.WriteLine("GenerateDtoTypes = true");
                Trace.WriteLine("UseBaseUrl = false");
                Trace.WriteLine("ClassStyle = CSharpClassStyle.Poco");
                Trace.WriteLine("UseDocumentTitle = true");

                InjectHttpClient = true;
                GenerateClientInterfaces = true;
                GenerateDtoTypes = true;
                UseBaseUrl = false;
                ClassStyle = CSharpClassStyle.Poco;
                UseDocumentTitle = true;
            }
        }

        [ExcludeFromCodeCoverage]
        private static INSwagOptions GetFromDialogPage()
            => (INSwagOptions)VsPackage.Instance.GetDialogPage(typeof(NSwagOptionsPage));

        public bool InjectHttpClient { get; }

        public bool GenerateClientInterfaces { get; }

        public bool GenerateDtoTypes { get; }

        public bool UseBaseUrl { get; }

        public CSharpClassStyle ClassStyle { get; }
        public bool UseDocumentTitle { get; }
    }
}