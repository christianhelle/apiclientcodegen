using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Rapicgen.Core.Converters;
using ICSharpCode.CodeConverter;

namespace Rapicgen.Converters
{
    [ExcludeFromCodeCoverage]
    public class CSharpToVisualBasicLanguageConverter : ILanguageConverter
    {
        public async Task<string> ConvertAsync(string code)
        {
            var options = new CodeWithOptions(code);
            var result = await CodeConverter.ConvertAsync(options);
            return result.ConvertedCode;
        }
    }
}
