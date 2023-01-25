using System;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options;

namespace Rapicgen.Options
{
    [ExcludeFromCodeCoverage]
    public class OptionsFactory : IOptionsFactory
    {
        public TOptions Create<TOptions, TDialogPage, TDefaultOptions>()
            where TOptions : class
            where TDefaultOptions : class, TOptions, new()
        {
            try
            {
                return VsPackage.Instance.GetDialogPage(typeof(TDialogPage)) as TOptions;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                return new TDefaultOptions();
            }
        }
    }
}