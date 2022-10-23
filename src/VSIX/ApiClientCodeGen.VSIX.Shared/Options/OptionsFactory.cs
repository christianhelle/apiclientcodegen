using System;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Options;

namespace Rapicgen.Options
{
    [ExcludeFromCodeCoverage]
    public class OptionsFactory : IOptionsFactory
    {
        public TOptions Create<TOptions, TDialogPage>()
            where TOptions : class
            => VsPackage.Instance.GetDialogPage(typeof(TDialogPage)) as TOptions ??
               throw new InvalidOperationException();
    }
}