namespace Rapicgen.Core.Options.NSwag
{
    public class DefaultNSwagOptions : INSwagOptions
    {
        public bool InjectHttpClient => true;
        public bool GenerateClientInterfaces => true;
        public bool GenerateDtoTypes => true;
        public CSharpClassStyle ClassStyle => CSharpClassStyle.Poco;
        public bool UseDocumentTitle => true;
        public string ParameterDateTimeFormat => "s";
        public bool UseBaseUrl => false;
    }
}