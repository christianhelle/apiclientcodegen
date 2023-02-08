using System;
using System.Diagnostics;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.AutoRest;

namespace Rapicgen.Options.AutoRest
{
    public class AutoRestOptions : OptionsBase<IAutoRestOptions, AutoRestOptionsPage>, IAutoRestOptions
    {
        public AutoRestOptions(IAutoRestOptions options)
        {
            try
            {
                if (options == null)
                    options = GetFromDialogPage();

                AddCredentials = options.AddCredentials;
                OverrideClientName = options.OverrideClientName;
                UseInternalConstructors = options.UseInternalConstructors;
                SyncMethods = options.SyncMethods;
                UseDateTimeOffset = options.UseDateTimeOffset;
                ClientSideValidation = options.ClientSideValidation;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);

                
                Logger.Instance.WriteLine(Environment.NewLine);
                Logger.Instance.WriteLine("Error reading user options. Reverting to default values");
                Logger.Instance.WriteLine($"AddCredentials = {AddCredentials}");
                Logger.Instance.WriteLine($"OverrideClientName = {OverrideClientName}");
                Logger.Instance.WriteLine($"UseInternalConstructors = {UseInternalConstructors}");
                Logger.Instance.WriteLine($"SyncMethods = {SyncMethods}");
                Logger.Instance.WriteLine($"UseDateTimeOffset = {UseDateTimeOffset}");
                Logger.Instance.WriteLine($"UseDateTimeOClientSideValidationffset = {ClientSideValidation}");

                AddCredentials = false;
                OverrideClientName = false;
                UseInternalConstructors = false;
                SyncMethods = SyncMethodOptions.Essential;
                UseDateTimeOffset = false;
                ClientSideValidation = true;
            }
        }

        public bool AddCredentials { get; set; }
        public bool OverrideClientName { get; set; }
        public bool UseInternalConstructors { get; set; }
        public SyncMethodOptions SyncMethods { get; set; }
        public bool UseDateTimeOffset { get; set; }
        public bool ClientSideValidation { get; set; }
    }
}