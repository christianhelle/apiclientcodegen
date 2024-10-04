using System;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Kiota;

namespace Rapicgen.Options.Kiota
{
    public class KiotaOptions
        : OptionsBase<IKiotaOptions, KiotaOptionsPage>, IKiotaOptions
    {
        public KiotaOptions(IKiotaOptions? options = null)
        {
            try
            {
                options ??= GetFromDialogPage();
                GenerateMultipleFiles = options.GenerateMultipleFiles;
                TypeAccessModifier = options.TypeAccessModifier;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);

                Logger.Instance.WriteLine(Environment.NewLine);
                Logger.Instance.WriteLine("Error reading user options. Reverting to default values");
                Logger.Instance.WriteLine("GenerateMultipleFiles = false");

                GenerateMultipleFiles = false;
                TypeAccessModifier = TypeAccessModifier.Public;
            }
        }

        public bool GenerateMultipleFiles { get; }
        public TypeAccessModifier TypeAccessModifier { get; }
    }
}