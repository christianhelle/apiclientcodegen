using System.Threading.Tasks;
using ICSharpCode.CodeConverter;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters
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
