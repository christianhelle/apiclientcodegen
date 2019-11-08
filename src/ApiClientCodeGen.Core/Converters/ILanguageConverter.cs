using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters
{
    public interface ILanguageConverter
    {
        Task<string> ConvertAsync(string code);
    }
}