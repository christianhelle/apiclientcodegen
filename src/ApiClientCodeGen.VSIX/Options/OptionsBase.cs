using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    [ExcludeFromCodeCoverage]
    public abstract class OptionsBase<TOptionsInterface, TOptionsPage>
        where TOptionsInterface : class
    {
        protected TOptionsInterface GetFromDialogPage()
            => VsPackage.Instance.GetDialogPage(typeof(TOptionsPage))
                as TOptionsInterface;
    }
}