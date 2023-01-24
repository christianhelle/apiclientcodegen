namespace Rapicgen.Core.Options
{
    public interface IOptionsFactory
    {
        TOptions Create<TOptions, TDialogPage, TDefaultOptions>()
            where TOptions : class
            where TDefaultOptions : class, TOptions, new();
    }
}