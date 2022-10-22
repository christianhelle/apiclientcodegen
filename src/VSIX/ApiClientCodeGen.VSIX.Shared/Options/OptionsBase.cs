using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Shell;

namespace Rapicgen.Options
{
    [ExcludeFromCodeCoverage]
    public abstract class OptionsBase<TOptionsInterface, TOptionsPage>
        where TOptionsInterface : class
        where TOptionsPage : DialogPage
    {
        protected TOptionsInterface GetFromDialogPage()
            => VsPackage.Instance.GetDialogPage(typeof(TOptionsPage))
                as TOptionsInterface;
    }
}