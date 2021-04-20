using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.AutoRest
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
                
                TraceLogger.WriteLine(Environment.NewLine);
                TraceLogger.WriteLine("Error reading user options. Reverting to default values");
                TraceLogger.WriteLine($"AddCredentials = {AddCredentials}");
                TraceLogger.WriteLine($"OverrideClientName = {OverrideClientName}");
                TraceLogger.WriteLine($"UseInternalConstructors = {UseInternalConstructors}");
                TraceLogger.WriteLine($"SyncMethods = {SyncMethods}");
                TraceLogger.WriteLine($"UseDateTimeOffset = {UseDateTimeOffset}");
                TraceLogger.WriteLine($"UseDateTimeOClientSideValidationffset = {ClientSideValidation}");

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