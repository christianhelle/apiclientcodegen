using Microsoft.VisualStudio.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class OptionsFactory : IOptionsFactory
    {
        public TOptions Create<TOptions, TDialogPage>()
            where TOptions : class
            where TDialogPage : DialogPage
            => VsPackage.Instance.GetDialogPage(typeof(TDialogPage)) as TOptions;
    }
}
