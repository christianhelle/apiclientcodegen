using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters;
using ICSharpCode.CodeConverter;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility
{
    internal class CSharpToVisualBasicLanguageConverter : ILanguageConverter
    {
        public async Task<string> Convert(string code)
        {
            var options = new CodeWithOptions(code);
            var result = await CodeConverter.Convert(options);
            return result.ConvertedCode;
        }
    }
}
