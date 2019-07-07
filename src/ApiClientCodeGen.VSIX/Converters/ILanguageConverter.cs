using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters
{
    public interface ILanguageConverter
    {
        Task<string> ConvertAsync(string code);
    }
}