using System;
using System.Diagnostics;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class NSwagCSharpOptions
    {
        public NSwagCSharpOptions()
        {
            try
            {
                var dialogPage = (OptionPageGrid)VsPackage.Instance.GetDialogPage(typeof(OptionPageGrid));
                InjectHttpClient = dialogPage.InjectHttpClient;
                GenerateClientInterfaces = dialogPage.GenerateClientInterfaces;
                GenerateDtoTypes = dialogPage.GenerateDtoTypes;
                UseBaseUrl = dialogPage.UseBaseUrl;
                ClassStyle = dialogPage.ClassStyle;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

                InjectHttpClient = true;
                GenerateClientInterfaces = true;
                GenerateDtoTypes = true;
                UseBaseUrl = false;
                ClassStyle = CSharpClassStyle.Poco;
            }
        }

        public bool InjectHttpClient { get; }

        public bool GenerateClientInterfaces { get; }

        public bool GenerateDtoTypes { get; }

        public bool UseBaseUrl { get; }

        public CSharpClassStyle ClassStyle { get; }
    }
}