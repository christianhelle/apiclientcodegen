using System.Threading.Tasks;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters
{
    public interface ILanguageConverter
    {
        Task<string> Convert(string code);
    }
}