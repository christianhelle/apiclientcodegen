using System.Threading.Tasks;
using ICSharpCode.CodeConverter;

namespace Rapicgen.Core.Converters
{
    public class CSharpToVisualBasicLanguageConverter : ILanguageConverter
    {
        public async Task<string> ConvertAsync(string code)
        {
            var options = new CodeWithOptions(code);
            var result = await CodeConverter.ConvertAsync(options);
            return result.ConvertedCode ?? string.Empty;
        }
    }
}
