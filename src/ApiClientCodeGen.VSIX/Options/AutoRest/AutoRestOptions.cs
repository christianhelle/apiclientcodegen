using System;
using System.Diagnostics;
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
                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Error reading user options. Reverting to default values");
                Trace.WriteLine($"AddCredentials = {AddCredentials}");
                Trace.WriteLine($"OverrideClientName = {OverrideClientName}");
                Trace.WriteLine($"UseInternalConstructors = {UseInternalConstructors}");
                Trace.WriteLine($"SyncMethods = {SyncMethods}");
                Trace.WriteLine($"UseDateTimeOffset = {UseDateTimeOffset}");
                Trace.WriteLine($"UseDateTimeOClientSideValidationffset = {ClientSideValidation}");

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