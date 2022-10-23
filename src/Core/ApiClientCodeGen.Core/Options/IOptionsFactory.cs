namespace Rapicgen.Core.Options
{
    public interface IOptionsFactory
    {
        TOptions Create<TOptions, TDialogPage>()
            where TOptions : class;
    }
}