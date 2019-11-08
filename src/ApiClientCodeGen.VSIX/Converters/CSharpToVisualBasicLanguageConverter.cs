using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters;
using ICSharpCode.CodeConverter;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters
{
    public class CSharpToVisualBasicLanguageConverter : ILanguageConverter
    {
        public async Task<string> ConvertAsync(string code)
        {
            var options = new CodeWithOptions(code);
            var result = await CodeConverter.Convert(options);
            return result.ConvertedCode;
        }
    }
}
