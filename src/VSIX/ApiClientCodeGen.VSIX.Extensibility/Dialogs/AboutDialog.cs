using Microsoft.VisualStudio.Extensibility.UI;
using System.Runtime.Serialization;

namespace ApiClientCodeGen.VSIX.Extensibility.Dialogs;

internal class AboutDialog : RemoteUserControl
{
    public AboutDialog(string displayName, string description, string version, string publisher, string extensionId, string supportKey)
        : base(new AboutDialogData(displayName, description, version, publisher, extensionId, supportKey))
    {
    }

    [DataContract]
    internal class AboutDialogData
    {
        public AboutDialogData(string displayName, string description, string version, string publisher, string extensionId, string supportKey)
        {
            DisplayName = displayName;
            Description = description;
            Version = version;
            Publisher = publisher;
            ExtensionId = extensionId;
            SupportKey = supportKey;
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
    }
}
