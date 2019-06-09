namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public interface IOptionsFactory
    {
        TOptions Create<TOptions, TDialogPage>() 
            where TOptions : class;
    }
}