using Rapicgen.Core.Converters;
using CoreConverter = Rapicgen.Core.Converters.CSharpToVisualBasicLanguageConverter;

namespace Rapicgen.Converters
{
    /// <summary>
    /// Wrapper for backward compatibility. Uses the Core implementation.
    /// </summary>
    public class CSharpToVisualBasicLanguageConverter : CoreConverter, ILanguageConverter
    {
    }
}
