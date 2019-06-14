namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class OptionsFactory : IOptionsFactory
    {
        public TOptions Create<TOptions, TDialogPage>() 
            where TOptions : class 
            => VsPackage.Instance.GetDialogPage(typeof(TDialogPage)) as TOptions;
    }
}
