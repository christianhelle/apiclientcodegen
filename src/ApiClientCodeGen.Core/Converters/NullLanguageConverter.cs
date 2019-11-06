using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters
{
    [ExcludeFromCodeCoverage]
    public class NullLanguageConverter : ILanguageConverter
    {
        public Task<string> ConvertAsync(string code) => Task.FromResult(code);
    }
}