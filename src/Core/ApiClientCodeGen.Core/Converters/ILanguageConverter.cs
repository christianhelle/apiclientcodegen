using System.Threading.Tasks;

namespace Rapicgen.Core.Converters
{
    public interface ILanguageConverter
    {
        Task<string> ConvertAsync(string code);
    }
}