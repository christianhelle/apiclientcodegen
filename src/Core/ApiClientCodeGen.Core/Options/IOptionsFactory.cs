namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options
{
    public interface IOptionsFactory
    {
        TOptions Create<TOptions, TDialogPage>()
            where TOptions : class;
    }
}