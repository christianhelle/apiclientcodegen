using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters
{
    public class NullLanguageConverter : ILanguageConverter
    {
        public Task<string> ConvertAsync(string code) => Task.FromResult(code);
    }
}