using System.Threading.Tasks;
using ICSharpCode.CodeConverter;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters
{
    internal class CSharpToVisualBasicLanguageConverter : ILanguageConverter
    {
        public async Task<string> ConvertAsync(string code)
        {
            var options = new CodeWithOptions(code);
            var result = await CodeConverter.Convert(options);
            return result.ConvertedCode;
        }
    }
}
