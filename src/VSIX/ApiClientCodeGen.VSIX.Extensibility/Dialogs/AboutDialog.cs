using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.UI;
using System.Runtime.Serialization;
using TextCopy;

namespace ApiClientCodeGen.VSIX.Extensibility.Dialogs;

internal class AboutDialog : RemoteUserControl
{
    public AboutDialog(VisualStudioExtensibility extensibility, string displayName, string description, string version, string publisher, string extensionId, string supportKey, string? analyticsDescription = null)
        : base(new AboutDialogData(extensibility, displayName, description, version, publisher, extensionId, supportKey, analyticsDescription))
    {
    }

    [DataContract]
    internal class AboutDialogData : NotifyPropertyChangedObject
    {
        private readonly VisualStudioExtensibility extensibility;

        public AboutDialogData(VisualStudioExtensibility extensibility, string displayName, string description, string version, string publisher, string extensionId, string supportKey, string? analyticsDescription = null)
        {
            this.extensibility = extensibility;
            DisplayName = displayName;
            Description = description;
            Version = version;
            Publisher = publisher;
            ExtensionId = extensionId;
            SupportKey = supportKey;
            AnalyticsDescription = analyticsDescription;
            CopySupportKeyCommand = new AsyncCommand(CopySupportKeyAsync);
        }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public string Publisher { get; set; }

        [DataMember]
        public string ExtensionId { get; set; }

        [DataMember]
        public string SupportKey { get; set; }

        [DataMember]
        public string? AnalyticsDescription { get; set; }

        [DataMember]
        public IAsyncCommand CopySupportKeyCommand { get; }

        private async Task CopySupportKeyAsync(object? parameter, CancellationToken cancellationToken)
        {
            await ClipboardService.SetTextAsync(SupportKey);
        }
    }
}
